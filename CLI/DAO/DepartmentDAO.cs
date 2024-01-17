using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;
using System.Xml.Linq;
using CLI.Observer;
using CLI.Controller;

namespace StudentskaSluzba.DAO
{
    public class DepartmentDAO
    {
        private readonly List<Department> departments;
        private readonly Storage<Department> storage;
       // private static ProfessorDAO professorDAO= new ProfessorDAO();
        public SubjectOB DepartmentSubject;

        public DepartmentDAO()
        {
            storage = new Storage<Department>("departments.txt");
            departments = storage.Load();
            DepartmentSubject = new SubjectOB();
        }

        public void save()
        {
            storage.Save(departments);
        }

        private int GenerateId()
        {
            if (departments.Count == 0)
                return 0;
            return departments[^1].Id + 1;
        }

        public Department AddDepartment(Department department)
        {
            foreach (Department dep in departments)
            {
                if (dep.Idd == department.Idd)
                {

                    System.Console.WriteLine("Department can't be added, because it already exists.");
                    return department;

                }
            }

            department.Id = GenerateId();
            departments.Add(department);
            storage.Save(departments);
            DepartmentSubject.NotifyObservers();
            return department;
        }

        public Department? UpdateDepartment(Department department)
        {
            Department? oldDepartment = GetDepartmentById(department.Id);
            if (oldDepartment is null) return null;

            oldDepartment.Idd = department.Idd;
            oldDepartment.Name = department.Name;
            oldDepartment.Hod = department.Hod;
            oldDepartment.Department_Professors = department.Department_Professors;

            storage.Save(departments);
            System.Console.WriteLine("Department is updated");
            DepartmentSubject.NotifyObservers();
            return oldDepartment;
        }

        public Department? RemoveDepartment(int id)
        {
            
            Department? oldDepartment = GetDepartmentById(id);
            if (oldDepartment is null) return null;

            departments.Remove(oldDepartment);
            storage.Save(departments);
            DepartmentSubject.NotifyObservers();
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

        public void addProfessor(ProfessorDAO professorDAO, int id, int departmentID)
        {
            
            Professor pro = professorDAO.GetProfessorById(id); //provera fali

           
            Department dep = GetDepartmentById(departmentID);


            dep.Department_Professors.Add(pro.Id);
            DepartmentSubject.NotifyObservers();


        }
        public bool doesDepartmentExist(int id)
        {

            Department dep = GetDepartmentById(id);
            return dep != null;
        }

        public List<int> GetProfessorList(int id)
        {
            Department dep = GetDepartmentById(id);
            return dep.Department_Professors;

        }
       
    }
}
