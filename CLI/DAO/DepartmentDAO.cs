﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.DAO
{
    class DepartmentDAO
    {
        private readonly List<Department> departments;
        private readonly Storage<Department> storage;

        public DepartmentDAO(string filename)
        {
            storage = new Storage<Department>(filename + ".txt");
            departments = storage.Load();
        }

        private int GenerateId()
        {
            if (departments.Count == 0)
                return 0;
            return departments[^1].Id + 1;
        }

        public Department AddDepartment(Department department)
        {
            department.Id = GenerateId();
            departments.Add(department);
            storage.Save(departments);
            return department;
        }

        public Department? UpdateDepartment(Department department)
        {
            Department? oldDepartment = GetDepartmentById(department.Id);
            if (oldDepartment is null) return null;

            // add updates

            storage.Save(departments);
            return oldDepartment;
        }

        public Department? RemoveDepartment(int id)
        {
            Department? oldDepartment = GetDepartmentById(id);
            if (oldDepartment is null) return null;

            departments.Remove(oldDepartment);
            storage.Save(departments);
            return oldDepartment;
        }

        public Department? GetDepartmentById(int id)
        {
            return departments.Find(d => d.Id == id);
        }
                
        public List<Department> GetAllDepartments()
        {
            return departments;
        }
    }
}