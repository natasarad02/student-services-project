﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.DAO;
using CLI.Observer;

namespace CLI.Controller
{
    public class ExamGradesController
    {
        private readonly ExamGradeDAO examGradeDAO;

        public ExamGradesController()
        {
            examGradeDAO = new ExamGradeDAO();
        }

        public List<ExamGrade> GetAllExamGrades()
        {
            return examGradeDAO.GetAllExamGrades();
        }

        public void Add(ExamGrade examGrade)
        {
            examGradeDAO.AddExamGrade(examGrade);
        }

        public void Delete(int examGradeId)
        {
            examGradeDAO.RemoveExamGrade(examGradeId);
        }

        public void Subscribe(IObserver observer)
        {
            examGradeDAO.ExamGradeSubject.Subscribe(observer);
        }
    }
}
