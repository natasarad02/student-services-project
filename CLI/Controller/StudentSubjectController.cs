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
    public class StudentsSubjectsController
    {
        private static StudentsSubjectsDAO studentsSubjectsDAO;

        public StudentsSubjectsController()
        {
            studentsSubjectsDAO = new StudentsSubjectsDAO();

        }

        public List<StudentsSubjects> GetAllStudentsSubjects()
        {
            return studentsSubjectsDAO.GetAllStudentsSubjectss();
        }

        public void Add(int id_student, int id_subject)
        {
            StudentsSubjects connection = new StudentsSubjects(id_student, id_subject);
            studentsSubjectsDAO.AddStudentsSubjects(connection);
           
        }

        public void Delete(int studId, int subId)
        {
            studentsSubjectsDAO.RemoveStudentsSubjects(studId, subId);
        }

        public void Subscribe(IObserver observer)
        {
            studentsSubjectsDAO.StudentSubjectSubject.Subscribe(observer);
        }

        public void Save()
        {
            studentsSubjectsDAO.save();
        }

        public List<Subject> GetAllSubjectsById(int id)
        {
            return studentsSubjectsDAO.GetSubjects(id);
        }

    }
}
