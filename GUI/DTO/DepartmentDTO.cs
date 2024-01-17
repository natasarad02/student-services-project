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

        private int idd;

        public int Idd
        {
            get
            {
                return idd;
            }
            set
            {
                if(value != idd)
                {
                    idd = value;
                    OnPropertyChanged("Idd");
                }
              
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

        public int hod_id;

        public int Hod_id
        {
            get
            {
                return hod_id;
            }
            set
            {
                if (value != hod_id)
                {
                    hod_id = value;
                    OnPropertyChanged("Hod_id");
                }

            }
        }


        private List<int> department_Professors;
        public List<int> Department_Professors
        {
            get
            {
                return department_Professors;
            }

            set
            {
                if (value != department_Professors)
                {
                    department_Professors = value;
                    OnPropertyChanged(nameof(Department_Professors));
                }
            }
        }

        public Department ToDepartment()
        {
            Department d = new Department(idd, name, hod);
            d.Id = id;
            d.Department_Professors = department_Professors;
            d.Hod_id = hod_id;
            return d;
        }

        public DepartmentDTO() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DepartmentDTO(Department department)
        {
            id = department.Id;
            idd = department.Idd;
            name = department.Name;
            hod = department.Hod;
            Department_Professors = department.Department_Professors;
            hod_id = department.Hod_id;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }


    }
