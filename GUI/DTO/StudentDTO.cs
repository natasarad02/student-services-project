using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentskaSluzba.Model;
using System.Windows.Data;
using System.Xml.Linq;
using System.Dynamic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged, IDataErrorInfo
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

        private string last_Name;
        public string Last_Name
        {
            get { return last_Name; }
            set
            {
                if (value != last_Name)
                {
                    last_Name = value;
                    OnPropertyChanged("Last_Name");
                }
            }
        }

        private string first_Name;
        public string First_Name
        {
            get { return first_Name; }
            set
            {
                if (value != first_Name)
                {
                    first_Name = value;
                    OnPropertyChanged("First_Name");
                }
            }
        }

        private DateTime date_Of_Birth;
        public DateTime Date_Of_Birth
        {
            get { return date_Of_Birth; }
            set
            {
                if (value != date_Of_Birth)
                {
                    date_Of_Birth = value;
                    OnPropertyChanged("Date_Of_Birth");
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
                    OnPropertyChanged("Address");
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

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private StudentskaSluzba.Model.Index index_Number;
        public StudentskaSluzba.Model.Index Index_Number
        {
            get { return index_Number; }
            set
            {
                if (value != index_Number)
                {
                    index_Number = value;
                    OnPropertyChanged(nameof(Index_Number));
                }
            }
        }

        public string Index_Number_String => $"{Index_Number.college_major}-{Index_Number.number_mark}-{Index_Number.YOE}";

        private int current_Year;
        public int Current_Year
        {
            get { return current_Year; }
            set
            {
                if (value != current_Year)
                {
                    current_Year = value;
                    OnPropertyChanged("Current_Year");
                }
            }
        }

        private Status status;
        public Status Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public float average_Grade;

        public float Average_Grade
        {
            get
            {
                return average_Grade;
            }

            set
            {
                if(value != average_Grade)
                {
                    average_Grade = value;
                    OnPropertyChanged(nameof(Average_Grade));
                }
            }
        }

      //  private Regex PhoneRegex = new Regex(@"+381[0-9]{6,6}[0-9]?");
        private Regex EmailRegex = new Regex(@"[a-zA-Z0-9._%+-]+@uns.ac.rs");
        public string Average_Grade_String => Average_Grade.ToString("F2");
        public string this[string columnName]
        {
            get
            {
              

               
                if (columnName == "First_Name")
                {
                    if (string.IsNullOrEmpty(First_Name))
                        return "First name is required";

                    /* Match match = _NameRegex.Match(Name);
                     if (!match.Success)
                         return "Format not good. Try again.";*/

                }
                else if (columnName == "Last_Name")
                {

                    if (string.IsNullOrEmpty(Last_Name))
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
                        return "Format should be +381xxxxxxxxx";*/

                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "E-Mail is required";
                    Match match = EmailRegex.Match(Email);
                    if (!match.Success)
                        return "E-Mail should end with uns.ac.rs";

                }
              /*  else if (columnName == "Current_Year")
                {
                    if (Current_Year == null)
                        return "Student's year is required";



                }
                else if (columnName == "Status")
                {
                    if (Status == null)
                        return "Status is required";



                }*/
               /* else if (columnName == "Address.Street")
                {
                    if (string.IsNullOrEmpty(Address.Street))
                        return "Street is required";
                }
                else if (columnName == "Address.Country")
                {
                    if (string.IsNullOrEmpty(Address.Country))
                        return "Country is required";
                }
                else if (columnName == "Address.City")
                {
                    if (string.IsNullOrEmpty(Address.City))
                        return "City is required";
                }
                else if (columnName == "Address.Number")
                {
                    if (Address.Number == 0)
                        return "Street number is required";
                }*/



                return null;
            }
        }
        public string Error => null;
        private readonly string[] _validatedProperties = { "First_Name", "Last_Name", "Email", "Phone_Number"};
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

        public List<ExamGrade> Passed_Exams { get; set; } //videti posle sta s ovim
        public List<ExamGrade> Failed_Exams { get; set; } //videti posle sta s ovim
        public bool Is_Deleted { get; set; }

        public StudentDTO() {

            index_Number = new StudentskaSluzba.Model.Index();
            address = new Address();
        
        }

        public Student toStudent() {
            Student s = new Student(last_Name, first_Name, date_Of_Birth, address, phone_Number, email, index_Number, current_Year, status);
            s.ID = id;
            return s;
        }

        public StudentDTO(Student student) {
            last_Name = student.Last_Name;
            first_Name = student.First_Name;
            date_Of_Birth = student.Date_Of_Birth;
            address = student.Address;
            phone_Number = student.Phone_Number;
            email = student.Email;
            index_Number = student.index_number;
            current_Year = student.Current_Year;
            status = student.Status;
            average_Grade = student.Average_Grade;
            Passed_Exams = new List<ExamGrade>();
            Failed_Exams = new List<ExamGrade>();
            Is_Deleted = false; // not deleted when created
            id = student.ID;
        }

        public bool Equals(StudentDTO other)
        {
            if (other is null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StudentDTO);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public string getIndeks() {
            return Index_Number.ToString();
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
/*public StudentDTO(string lname, string fname, DateOnly brdate, Address adr, string num, string emails, StudentskaSluzba.Model.Index idnum, int cyear, Status s)
        {
            last_Name = lname;
            first_Name = fname;
            date_Of_Birth = brdate;
            address = adr;
            phone_Number = num;
            email = emails;
            index_Number = idnum;
            current_Year = cyear;
            status = s;
            average_Grade = 0; // no grades yet
            Passed_Exams = new List<ExamGrade>();
            Failed_Exams = new List<ExamGrade>();
            Is_Deleted = false; // not deleted when created
        }*/