using System;
using System.ComponentModel;

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
                collegeMajor = value;
                OnPropertyChanged("CollegeMajor");
            }
        }

        private int numberMark;

        public int NumberMark
        {
            get { return numberMark; }
            set
            {
                numberMark = value;
                OnPropertyChanged("NumberMark");
            }
        }

        private int yoe;

        public int Yoe
        {
            get { return yoe; }
            set
            {
                yoe = value;
                OnPropertyChanged("Yoe");
            }
        }

        public IndexDTO() { }

        public IndexDTO(StudentskaSluzba.Model.Index index)
        {
            collegeMajor = index.college_major;
            numberMark = index.number_mark;
            yoe = index.YOE;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
