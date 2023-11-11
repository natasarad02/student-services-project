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
    class ExamGradeDAO
    {
        private readonly List<ExamGrade> examGrades;
        private readonly Storage<ExamGrade> storage;

        public ExamGradeDAO(string filename)
        {
            storage = new Storage<ExamGrade>(filename + ".txt");
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

            // add updates

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
    }
}
