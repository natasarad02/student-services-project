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
        private readonly StudentsSubjectsDAO studentsSubjectsDAO;

        public StudentsSubjectsController()
        {
            studentsSubjectsDAO = new StudentsSubjectsDAO();

        }

        public List<StudentsSubjects> GetAllStudentsSubjects()
        {
            return studentsSubjectsDAO.GetAllStudentsSubjectss();
        }

        public void Add(StudentsSubjects stud_sub)
        {
            studentsSubjectsDAO.AddStudentsSubjects(stud_sub);
        }

        public void Delete(int studId, int subId)
        {
            studentsSubjectsDAO.RemoveStudentsSubjects(studId, subId);
        }

        public void Subscribe(IObserver observer)
        {
            studentsSubjectsDAO.StudentSubjectSubject.Subscribe(observer);
        }
    }
}
