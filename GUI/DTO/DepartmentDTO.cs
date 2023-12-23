using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StudentskaSluzba.Model;

namespace GUI.DTO
{
    public class DepartmentDTO: INotifyPropertyChanged
    {
        private int Id;

        private int idd;

        public int Idd
        {
            get
            {
                return idd;
            }
            set
            {
                idd = value;
                OnPropertyChanged("Idd");
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string hod;

        public string Hod
        {
            get
            {
                return hod;
            }
            set
            {
                if (value != hod)
                {
                    hod = value;
                    OnPropertyChanged("Hod");
                }
            }
        }

        private List<Professor> department_Professors;

        public List<Professor> Department_Professors { get; set; } //videti posle sta s ovim

        public Department ToDepartment()
        {
            return new Department(idd, name, hod);
        }

        public DepartmentDTO() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DepartmentDTO(Department department)
        {
            Id = department.Id;
            idd = department.Idd;
            name = department.Name;
            hod = department.Hod;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

       

        }


    }
