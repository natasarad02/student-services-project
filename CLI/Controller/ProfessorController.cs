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
    public class ProfessorsController
    {
        private readonly ProfessorDAO professorsDAO;

        public ProfessorsController()
        {
            professorsDAO = new ProfessorDAO();

        }

        public List<Professor> GetAllProfessors()
        {
            return professorsDAO.GetAllProfessors();
        }

        public void Add(Professor professor)
        {
            professorsDAO.AddProfessor(professor);
        }

        public void Delete(int professorId)
        {
            professorsDAO.RemoveProfessor(professorId);
        }

        public void Update(Professor professor)
        {
            professorsDAO.UpdateProfessor(professor);
        }

        public void Subscribe(IObserver observer)
        {
            professorsDAO.ProfessorSubject.Subscribe(observer);
        }

        public void Save()
        {
            professorsDAO.save();
        }

        public List<Professor> GetProfessorsPage(int page, int itemsPerPage)
        {

            List<Professor> allProfessors = GetAllProfessors();


            int startIndex = page * itemsPerPage;


            var ProfessorsPage = allProfessors.Skip(startIndex).Take(itemsPerPage).ToList();

            return ProfessorsPage;
        }
    }
}
