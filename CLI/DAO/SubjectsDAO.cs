﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
using CLI.Observer;

namespace StudentskaSluzba.DAO
{
    public class SubjectDAO
    {
        private readonly List<Subject> subjects;
        private readonly Storage<Subject> storage;
        private static StudentsSubjectsDAO studentsSubjectsDAO = new StudentsSubjectsDAO();
        private static ExamGradeDAO examGradeDAO1 = new ExamGradeDAO();
        public SubjectOB SubjectSubject;
        public SubjectDAO()
        {
            storage = new Storage<Subject>("subjects.txt");
            subjects = storage.Load();
            SubjectSubject = new SubjectOB();
        }

        public void save()
        {
            storage.Save(subjects);
        }

        private int GenerateId()
        {
            if (subjects.Count == 0)
                return 0;
            return subjects[^1].Id + 1;
        }

        public Subject AddSubject(Subject subject)
        {

            foreach(Subject sub in subjects)
            {
                if(sub.Ids == subject.Ids)
                {
                    
                    System.Console.WriteLine("Subject can't be added, because it already exists.");
                    return subject;

                }
            }
         
           
            subject.Id = GenerateId();
            if(subject.ProfessorName == null)
            {
                subject.ProfessorID = -1;
            }
            subjects.Add(subject);
            storage.Save(subjects);
            SubjectSubject.NotifyObservers();
            System.Console.WriteLine("Subject is added");
            return subject;
        }

        public Subject? UpdateSubject(Subject subject)
        {
            Subject? oldSubject = GetSubjectById(subject.Id);
            if (oldSubject is null) return null;

            oldSubject.Ids = subject.Ids;
            oldSubject.Name = subject.Name;
            oldSubject.semester = subject.semester;
            oldSubject.year = subject.year;
            
            oldSubject.ProfessorName = subject.ProfessorName;
            if(subject.ProfessorName == null)
            {
                oldSubject.ProfessorID = -1;
                oldSubject.ProfessorName = "";
            }
            else
            {
                oldSubject.ProfessorID = subject.ProfessorID;
            }
            
            oldSubject.Espb = subject.Espb;
            oldSubject.Students_passed = subject.Students_passed;
            oldSubject.Students_attending = subject.Students_attending;

            storage.Save(subjects);
            SubjectSubject.NotifyObservers();
            System.Console.WriteLine("Subject updated");
            return oldSubject;
        }

        public Subject? RemoveSubject(int id)
        {
            Subject? oldSubject = GetSubjectById(id);
            if (oldSubject is null) return null;

            subjects.Remove(oldSubject);
            storage.Save(subjects);
            SubjectSubject.NotifyObservers();
            return oldSubject;
        }

        public Subject? GetSubjectById(int id)
        {
            return subjects.Find(s => s.Id == id);
        }

        public List<Subject> GetAllSubjects()
        {
            return subjects;
        }

        public List<Subject> getSubjectsByProfID(int profID) //what does prof teach?
        {
            List<Subject> temp = subjects.FindAll(e => e.ProfessorID == profID);
            return temp;
        }

        public List<Student> GetStudents(int subjectId) //izbacuje vise
        {
            return studentsSubjectsDAO.GetStudents(subjectId);
        }
        
        public void addStudentSubject(StudentsSubjectsDAO studSubDAO, int id_student, int id_subject) //izbacuje vise
        {
            StudentsSubjects connection = new StudentsSubjects(id_student, id_subject);
            studSubDAO.AddStudentsSubjects(connection);
        }

        public void grade(ExamGradeDAO examGrDAO, int student, int subject, int grade, DateTime date)
        {
            ExamGrade exam = new ExamGrade(student, subject, grade, date);
            examGrDAO.AddExamGrade(exam);
            if (examGradeDAO1.grade_exists(student, subject))
            {
                studentsSubjectsDAO.RemoveStudentsSubjects(student, subject);
            }
        }

        public bool doesSubjectExist(int id)
        {
            Subject subject = subjects.Find(s => s.Id == id);
            if (subject == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<Subject> sortedSubject(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Subject> ssubjects = subjects;

            switch (sortCriteria)
            {
                case "Id":
                    ssubjects = subjects.OrderBy(x => x.Id);
                    break;

                case "Ids":
                    ssubjects = subjects.OrderBy(x => x.Ids);
                    break;
                case "Name":
                    ssubjects = subjects.OrderBy(x => x.Name);
                    break;
                case "ESPB":
                    ssubjects = subjects.OrderBy(x => x.Espb);
                    break;
                case "Year":
                    ssubjects = subjects.OrderBy(x => x.year);
                    break;
                case "Semester":
                    ssubjects = subjects.OrderBy(x => x.semester);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                ssubjects = ssubjects.Reverse();

            ssubjects = ssubjects.Skip((page - 1) * pageSize).Take(pageSize);

            return ssubjects.ToList();
        }

        public List<Subject> findAllWithoutProfessor() {
            //ones that dont have a professor

            List<Subject> temp = subjects.FindAll(e => e.ProfessorID == -1);
            return temp;

        }

    }
}
