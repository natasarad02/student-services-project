using System;
using System.Collections.Generic;
using System.ComponentModel;
using StudentskaSluzba.Model;


namespace GUI.DTO
{
    public class SubjectDTO : INotifyPropertyChanged
    {

        private int id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private int ids;
        public int Ids
        {
            get { return ids; }
            set
            {
                if (value != ids)
                {
                    ids = value;
                    OnPropertyChanged("Ids");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private int espb;
        public int Espb
        {
            get { return espb; }
            set
            {
                if (value != espb)
                {
                    espb = value;
                    OnPropertyChanged("Espb");
                }
            }
        }

        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (value != year)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        private semester sem;
        public semester semester
        {
            get { return sem; }
            set
            {
                if (value != sem)
                {
                    sem = value;
                    OnPropertyChanged("semester");
                }
            }
        }

        private int professorId;
        public int ProfessorId
        {
            get { return professorId; }
            set
            {
                if (value != professorId)
                {
                    professorId = value;
                    OnPropertyChanged("ProfessorId");
                }
            }
        }

        public List<StudentDTO> Students_Passed { get; set; }
        public List<StudentDTO> Students_Attending { get; set; }

        public Subject ToSubject()
        {
            Subject s= new Subject(ids, name, espb, sem, year, professorId);
            s.Id = id;
            return s;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public SubjectDTO()
        {

        }

        public SubjectDTO(Subject subject)
        {
            ids = subject.Ids;
            name = subject.Name;
            espb = subject.Espb;
            sem = subject.semester;
            year = subject.year;
            professorId = subject.ProfessorID;
            Students_Passed = new List<StudentDTO>();
            Students_Attending = new List<StudentDTO>();
            id = subject.Id;
        }

        
    }

   /* public enum semester
    {
        // Define semester enum values
    }*/
}