using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentskaSluzba.Model;
using System.Windows.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace GUI.DTO
{
    public class ProfessorDTO : INotifyPropertyChanged, IDataErrorInfo
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

        private AddressDTO address;
        public AddressDTO Address
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

        private DateTime birth_Date;
        public DateTime Birth_Date
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
        
       // private Regex PhoneRegex = new Regex(@"06[0-9]\/[0-9]{6,6}[0-9]?");
        private Regex EmailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@uns.ac.rs");
        public string this[string columnName]
        {
            get
            {
                /*if (string.IsNullOrEmpty(Address.Street))

                    return "Street is required";





                if (string.IsNullOrEmpty(Address.Country))
                    return "Street is required";

                if (Address.Number == 0)
                    return "Street number is required";*/

                if (columnName == "Num")
                {
                    if (Num <= 999999)
                        return "Enter a valid ID card number";
                }
                else if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "First name is required";

                    /* Match match = _NameRegex.Match(Name);
                     if (!match.Success)
                         return "Format not good. Try again.";*/

                }
                else if (columnName == "Surname")
                {

                    if (string.IsNullOrEmpty(Surname))
                        return "Last name is required";
                }

                /* Match match = _NameRegex.Match(Name);
                 if (!match.Success)
                     return "Format not good. Try again.";*/

                else if (columnName == "Phone_Number")
                {
                    if (string.IsNullOrEmpty(Phone_Number))
                        return "Phone number is required";
                  /*  Match match = PhoneRegex.Match(Phone_Number);
                    if (!match.Success)
                        return "Format should be 06x/xxxxxxx";*/

                }
                else if (columnName == "Email_Address")
                {
                    if (string.IsNullOrEmpty(Email_Address))
                        return "E-Mail is required";
                    Match match = EmailRegex.Match(Email_Address);
                    if (!match.Success)
                        return "E-Mail should end with uns.ac.rs";

                }
                else if (columnName == "Employment_Year")
                {
                    if (Employment_Year > DateTime.Today.Year || Employment_Year < 1960)
                        return "Enter a valid year";



                }
                else if (columnName == "Calling")
                {
                    if (string.IsNullOrEmpty(Calling))
                        return "Calling is required";



                }
               



                return null;
            }
        }
        public string Error => null;
        private readonly string[] _validatedProperties = { "Name", "Surname", "Email_Address", "Phone_Number", "Calling", "Employment_Year" };


        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        public List<Subject> Subjects { get; set; }


        public ProfessorDTO()
        {

            address = new AddressDTO();
        }

        public ProfessorDTO(Professor prof)
        {
            num = prof.num;
            name = prof.Name;
            surname = prof.Surname;
            address = new AddressDTO(prof.Address);
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
            Professor p= new Professor(num, name, surname, address.ToAddress(), phone_Number, birth_Date, employment_Year, email_Address,  calling);
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