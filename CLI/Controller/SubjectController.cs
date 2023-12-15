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
    public class SubjectsController
    {
        private readonly SubjectDAO subjectsDAO;

        public SubjectsController()
        {
            subjectsDAO = new SubjectDAO();

        }

        public List<Subject> GetAllSubjects()
        {
            return subjectsDAO.GetAllSubjects();
        }

        public void Add(Subject subject)
        {
            subjectsDAO.AddSubject(subject);
        }

        public void Delete(int professorId)
        {
            subjectsDAO.RemoveSubject(professorId);
        }

        public void Subscribe(IObserver observer)
        {
            subjectsDAO.SubjectSubject.Subscribe(observer);
        }
    }
}
