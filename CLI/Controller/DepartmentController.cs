﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.DAO;
using CLI.Observer;

namespace CLI.Controller
{
    public class DepartmentsController
    {
        private readonly DepartmentDAO departmentDAO;

        public DepartmentsController()
        {
            departmentDAO = new DepartmentDAO();
        }

        public List<Department> GetAllDepartments()
        {
            return departmentDAO.GetAllDepartments();
        }

        public void Add(Department department)
        {
            departmentDAO.AddDepartment(department);
        }

        public void Delete(int departmentId)
        {
            departmentDAO.RemoveDepartment(departmentId);
        }

        public void Subscribe(IObserver observer)
        {
            departmentDAO.DepartmentSubject.Subscribe(observer);
        }
    }
}