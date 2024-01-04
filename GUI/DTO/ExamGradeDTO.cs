using StudentskaSluzba.Model;
using System;
using System.ComponentModel;

namespace GUI.DTO
{
    public class ExamGradeDTO : INotifyPropertyChanged
    {
        private int myId;

        public int MyId
        { 
            get { return myId; } 
            set {  myId = value; 
                   OnPropertyChanged("MyId");
                }
        }


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

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int esbp;
        public int Esbp
        {
            get { return esbp; }
            set
            {
                esbp = value;
                OnPropertyChanged("Espb");
            }
        }

        private int subject_id;
        public int Subject_ID
        {
            get { return subject_id; }
            set
            {
                subject_id = value;
                OnPropertyChanged("Subject_ID");
            }
        }

        public ExamGradeDTO() { }

        public ExamGradeDTO(ExamGrade examGrade) {
            myId = examGrade.ID;
            studentID = examGrade.studentID;
            subjectID = examGrade.subjectID;    
            grade = examGrade.grade;
            gradingDay = examGrade.grading_day;
        }

        public ExamGradeDTO(ExamGrade examGrade, string name, int esbp, int ID)
        {
            myId = examGrade.ID;
            studentID = examGrade.studentID;
            subjectID = examGrade.subjectID;
            grade = examGrade.grade;
            gradingDay = examGrade.grading_day;
            this.name = name;
            this.esbp = esbp;
            this.subject_id = ID;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
