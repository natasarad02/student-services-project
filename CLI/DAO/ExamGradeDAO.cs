﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.DAO
{
    class ExamGradeDAO
    {
        private readonly List<ExamGrade> examGrades;
        private readonly Storage<ExamGrade> storage;
        private StudentDAO studentDAO = new StudentDAO();

        public ExamGradeDAO()
        {
            storage = new Storage<ExamGrade>("exam_grades.txt");
            examGrades = storage.Load();
        }

        private int GenerateId()
        {
            if (examGrades.Count == 0)
                return 0;
            return examGrades[^1].ID + 1;
        }

        public ExamGrade AddExamGrade(ExamGrade examGrade)
        {
            examGrade.ID = GenerateId();
            examGrades.Add(examGrade);
            storage.Save(examGrades);
            return examGrade;
        }

        public ExamGrade? UpdateExamGrade(ExamGrade examGrade)
        {
            ExamGrade? oldExamGrade = GetExamGradeById(examGrade.ID);
            if (oldExamGrade is null) return null;

            oldExamGrade.studentID = examGrade.studentID;
            oldExamGrade.subjectID = examGrade.subjectID;
            oldExamGrade.grade = examGrade.grade;
            oldExamGrade.grading_day = examGrade.grading_day;

            storage.Save(examGrades);
            return oldExamGrade;
        }

        public ExamGrade? RemoveExamGrade(int id)
        {
            ExamGrade? oldExamGrade = GetExamGradeById(id);
            if (oldExamGrade is null) return null;

            examGrades.Remove(oldExamGrade);
            storage.Save(examGrades);
            return oldExamGrade;
        }

        public ExamGrade? GetExamGradeById(int id)
        {
            return examGrades.Find(e => e.ID == id);
        }

        public List<ExamGrade> GetAllExamGrades()
        {
            return examGrades;
        }

        public List<ExamGrade> GetExamGradesByStudent(int s_id)
        {

            return examGrades.FindAll(e => e.studentID == s_id);
            
        }
       
        public List<Student> GetStudentBySubject(int sub_id)  // vraca sve studente koji su polozili
        {
            List<ExamGrade> temp = examGrades.FindAll(e => e.subjectID == sub_id);
            List<int> studentIDs = new List<int>();
            foreach (ExamGrade examGrade in temp)
            {
                studentIDs.Add(examGrade.studentID);
            }
            
            
            List<Student> students = new List<Student>();

            foreach(int studentID in studentIDs)
            {
                students.Add(studentDAO.getStudentbyId(studentID));
            }

            return students;


        }

        public bool grade_exists(int stud_id, int subj_id) 
        {
            ExamGrade test = examGrades.Find(e => e.studentID == stud_id && e.subjectID == subj_id);
            if(test == null)
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
