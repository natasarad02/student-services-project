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
    public class StudentsController
    {
        private readonly StudentDAO studentDAO;
      
        public StudentsController()
        {
            studentDAO = new StudentDAO();
           
        }

        public List<Student> GetAllStudents()
        {
            return studentDAO.GetAllStudents();
        }

        public void Add(Student student)
        {
            studentDAO.addStudent(student);
        }

        public void Delete(int studentId)
        {
            studentDAO.removeStudent(studentId);
        }

        public void Subscribe(IObserver observer)
        {
            studentDAO.StudentSubject.Subscribe(observer);
        }

        public void Update(Student student)
        {
            studentDAO.UpdateStudent(student);
        }

        public void Save() {
            studentDAO.save();
        }

        public List<Student> GetStudentsPage(int page, int itemsPerPage)
        {
            
            List<Student> allStudents = GetAllStudents();

            
            int startIndex = page * itemsPerPage;

            
            var studentsPage = allStudents.Skip(startIndex).Take(itemsPerPage).ToList();

            return studentsPage;
        }

        public List<Student> GetStudentsPageSearch(int page, int itemsPerPage, List<Student> searchResults)
        {

            List<Student> allStudents = searchResults;


            int startIndex = page * itemsPerPage;


            var studentsPage = allStudents.Skip(startIndex).Take(itemsPerPage).ToList();

            return studentsPage;
        }
        /* public void AddStudentSubject(int id_student, int id_subject)
         {
             studentDAO.addStudentSubject(studentsSubjectsDAO, id_student, id_subject);
         }*/
    }
}

