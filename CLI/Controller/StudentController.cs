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
        private readonly StudentsSubjectsDAO studentsSubjectsDAO;
        public StudentsController()
        {
            studentDAO = new StudentDAO();
            studentsSubjectsDAO = new StudentsSubjectsDAO();
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

        public void AddStudentSubject(int id_student, int id_subject)
        {
            studentDAO.addStudentSubject(studentsSubjectsDAO, id_student, id_subject);
        }
    }
}

