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

        private DateOnly gradingDay;

        public DateOnly GradingDay
        {
            get { return gradingDay; }
            set
            {
                gradingDay = value;
                OnPropertyChanged("GradingDay");
            }
        }

        public ExamGradeDTO() { }

        public ExamGradeDTO(int studId, int subId, int gr, DateOnly date)
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
