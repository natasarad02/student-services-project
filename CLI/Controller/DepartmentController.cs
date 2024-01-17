using System;
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

        public void Update(Department department)
        {
            departmentDAO.UpdateDepartment(department);
        }

        public void Subscribe(IObserver observer)
        {
            departmentDAO.DepartmentSubject.Subscribe(observer);
        }

        public void Save()
        {
            departmentDAO.save();
        }

        public List<Professor> GetProfessorsThatCouldBeHOD(int dep_id, ProfessorsController professorsController)
        {
            Department dep = departmentDAO.GetDepartmentById(dep_id);
            List<Professor> pass_criteria = new List<Professor>(); // vandredni ili redovni + rade preko 5 godina
            List<Professor> tmpProfessorList = new List<Professor>();
            foreach(int profId in dep.Department_Professors)
            {
                foreach (Professor prof in professorsController.GetAllProfessors())
                {
                    if(profId == prof.Id)
                    {
                        tmpProfessorList.Add(prof);
                        break;
                    }

                }
            }
            
            foreach (Professor prof in tmpProfessorList)
            {
                if ((prof.calling == "associate professor" || prof.calling == "professor") && (2024 - prof.employment_year) > 5)
                {
                    pass_criteria.Add(prof);
                }
            }
            return pass_criteria;
        }

        public List<int> getProfessorsByDepartmentId(int dep_id)
        {
            Department dep = departmentDAO.GetDepartmentById(dep_id);
           
            return dep.Department_Professors;

        }

        public void addProfessor(int  dep_id, int prof_id)
        {
           


            departmentDAO.addProfessor(prof_id, dep_id);

        }

        public List<Professor> getProfessorsByDepartmentProfessors(Department department, ProfessorsController professorsController)
        {
            List<Professor> tmpProfessor = new List<Professor>();
            foreach(int profID in department.Department_Professors)
            {
                foreach(Professor prof in professorsController.GetAllProfessors())
                {
                    if(prof.Id == profID)
                    {
                        tmpProfessor.Add(prof);
                        break;
                    }
                }
            }
            return tmpProfessor;

        }
    }
}