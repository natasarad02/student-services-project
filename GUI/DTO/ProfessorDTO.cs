using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentskaSluzba.Model;
using System.Windows.Data;

namespace GUI.DTO
{
    public class ProfessorDTO : INotifyPropertyChanged
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

        private int num;
        public int Num
        {
            get { return num; }
            set
            {
                if (value != num)
                {
                    num = value;
                    OnPropertyChanged("Num");
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

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        private Address address;
        public Address Address
        {
            get { return address; }
            set
            {
                if (value != address)
                {
                    address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }

        private DateOnly birth_Date;
        public DateOnly Birth_Date
        {
            get { return birth_Date; }
            set
            {
                if (value != birth_Date)
                {
                    birth_Date = value;
                    OnPropertyChanged("Birth_Date");
                }
            }
        }

        private string phone_Number;
        public string Phone_Number
        {
            get { return phone_Number; }
            set
            {
                if (value != phone_Number)
                {
                    phone_Number = value;
                    OnPropertyChanged("Phone_Number");
                }
            }
        }

        private int employment_Year;
        public int Employment_Year
        {
            get { return employment_Year; }
            set
            {
                if (value != employment_Year)
                {
                    employment_Year = value;
                    OnPropertyChanged("Employment_Year");
                    OnPropertyChanged("Work_Year");
                }
            }
        }

        public int Work_Year => DateTime.Now.Year - Employment_Year;

        private string email_Address;
        public string Email_Address
        {
            get { return email_Address; }
            set
            {
                if (value != email_Address)
                {
                    email_Address = value;
                    OnPropertyChanged("Email_Address");
                }
            }
        }

        private string calling;
        public string Calling
        {
            get { return calling; }
            set
            {
                if (value != calling)
                {
                    calling = value;
                    OnPropertyChanged("Calling");
                }
            }
        }

        public List<Subject> Subjects { get; set; }


        public ProfessorDTO()
        {

            address = new Address();
        }

        public ProfessorDTO(Professor prof)
        {
            num = prof.num;
            name = prof.Name;
            surname = prof.Surname;
            address = prof.Address;
            birth_Date = prof.birth_date;
            phone_Number = prof.phone_number;
            email_Address = prof.email_address;
            employment_Year = prof.employment_year;
            calling = prof.calling;
            Subjects = new List<Subject>();
            id = prof.Id;


        }
        public Professor ToProfessor()

        {
            Professor p= new Professor(num, name, surname, address, phone_Number, birth_Date, employment_Year, email_Address,  calling);
            p.Id = id;
            return p;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}