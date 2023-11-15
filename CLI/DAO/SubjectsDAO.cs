using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.DAO
{
    class SubjectDAO
    {
        private readonly List<Subject> subjects;
        private readonly Storage<Subject> storage;
        private StudentsSubjectsDAO studentsSubjectsDAO = new StudentsSubjectsDAO();
        private static ExamGradeDAO examGradeDAO1 = new ExamGradeDAO();

        public SubjectDAO()
        {
            storage = new Storage<Subject>("subjects.txt");
            subjects = storage.Load();
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
            subjects.Add(subject);
            storage.Save(subjects);
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
            oldSubject.ProfessorID = subject.ProfessorID;
            oldSubject.Espb = subject.Espb;
            oldSubject.Students_passed = subject.Students_passed;
            oldSubject.Students_attending = subject.Students_attending;

            storage.Save(subjects);
            System.Console.WriteLine("Subject updated");
            return oldSubject;
        }

        public Subject? RemoveSubject(int id)
        {
            Subject? oldSubject = GetSubjectById(id);
            if (oldSubject is null) return null;

            subjects.Remove(oldSubject);
            storage.Save(subjects);
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
            //dodati pri ispisu da ispisuje samo osnovne info tj BEZ LISTA
        }

        public List<Student> GetStudents(int subjectId) //izbacuje vise
        {
            return studentsSubjectsDAO.GetStudents(subjectId);
        }
        
        public void addStudentSubject(int id_student, int id_subject) //izbacuje vise
        {
            StudentsSubjects connection = new StudentsSubjects(id_student, id_subject);
            studentsSubjectsDAO.AddStudentsSubjects(connection);
        }

        public void grade(int student, int subject, int grade, DateOnly date)
        {
            ExamGrade exam = new ExamGrade(student, subject, grade, date);
            examGradeDAO1.AddExamGrade(exam);
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



    }
}
