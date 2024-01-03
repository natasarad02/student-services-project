using StudentskaSluzba.Model;
using System;
using System.ComponentModel;

namespace GUI.DTO
{
    public class ExamGradeDTO : INotifyPropertyChanged
    {
        private int studentID;

        public int StudentID
        {
            get { return studentID; }
            set
            {
                studentID = value;
                OnPropertyChanged("StudentID");
            }
        }

        private int subjectID;

        public int SubjectID
        {
            get { return subjectID; }
            set
            {
                subjectID = value;
                OnPropertyChanged("SubjectID");
            }
        }

        private int grade;

        public int Grade
        {
            get { return grade; }
            set
            {
                grade = value;
                OnPropertyChanged("Grade");
            }
        }

        private DateTime gradingDay;

        public DateTime GradingDay
        {
            get { return gradingDay; }
            set
            {
                gradingDay = value;
                OnPropertyChanged("GradingDay");
            }
        }

        public ExamGradeDTO() { }

        public ExamGradeDTO(ExamGrade examGrade) { 
            studentID = examGrade.studentID;
            subjectID = examGrade.subjectID;    
            grade = examGrade.grade;
            gradingDay = examGrade.grading_day;
        }

        public ExamGradeDTO(int studId, int subId, int gr, DateTime date)
        {
            studentID = studId;
            subjectID = subId;
            grade = gr;
            gradingDay = date;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
