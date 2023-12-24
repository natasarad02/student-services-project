using System;
using System.ComponentModel;
using StudentskaSluzba.Model;

namespace GUI.DTO
{
    public class IndexDTO : INotifyPropertyChanged
    {
        private string collegeMajor;

        public string CollegeMajor
        {
            get { return collegeMajor; }
            set
            {
                if (collegeMajor != value)
                {
                    collegeMajor = value;
                    OnPropertyChanged("CollegeMajor");
                }
            }
        }

        private int numberMark;

        public int NumberMark
        {
            get { return numberMark; }
            set
            {
                if (numberMark != value)
                {
                    numberMark = value;
                    OnPropertyChanged("NumberMark");
                }
            }
        }

        private int yoe;

        public int Yoe
        {
            get { return yoe; }
            set
            {
                if (yoe != value)
                {
                    yoe = value;
                    OnPropertyChanged("Yoe");
                }
            }
        }
        public StudentskaSluzba.Model.Index ToIndex()
        {
            StudentskaSluzba.Model.Index i = new StudentskaSluzba.Model.Index(collegeMajor, numberMark, yoe);
            return i;
        }
        public event PropertyChangedEventHandler? PropertyChanged;


  
        public IndexDTO() { }

        public IndexDTO(StudentskaSluzba.Model.Index index)
        {
            collegeMajor = index.college_major;
            numberMark = index.number_mark;
            yoe = index.YOE;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }




    }
}
