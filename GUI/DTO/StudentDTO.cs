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
    public class StudentDTO : INotifyPropertyChanged
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

        private DateOnly date_Of_Birth;
        public DateOnly Date_Of_Birth
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

        public double average_Grade;

        private double Average_Grade
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
                    OnPropertyChanged("Average_Grade");
                }
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
            average_Grade = 0; // no grades yet
            Passed_Exams = new List<ExamGrade>();
            Failed_Exams = new List<ExamGrade>();
            Is_Deleted = false; // not deleted when created
            id = student.ID;
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