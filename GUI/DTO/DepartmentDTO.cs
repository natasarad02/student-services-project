﻿using System;
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


        public List<Professor> Department_Professors { get; set; } //videti posle sta s ovim

        public Department ToDepartment()
        {
            Department d = new Department(idd, name, hod);
            d.Id = id;
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
            Department_Professors = new List<Professor>();
        }

        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }


    }
