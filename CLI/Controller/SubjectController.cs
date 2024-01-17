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
        public void Update(Subject subject)
        {
            subjectsDAO.UpdateSubject(subject);
        }
        public void Subscribe(IObserver observer)
        {
            subjectsDAO.SubjectSubject.Subscribe(observer);
        }

        public void Save()
        {
            subjectsDAO.save();
        }

        public Subject findSubjectById(int id) {
            return subjectsDAO.GetSubjectById(id);
        }

        public Professor getProfessorForSubject(Subject subject, List<Professor> allProfessors)
        {
           
            foreach (Professor professor in allProfessors)
            {
                if(professor.Id == subject.ProfessorID)
                {
                    return professor;
                }
            }
            return null;
         
        }

        public Subject GetSubjectById(int subjectId)
        {
            return subjectsDAO.GetSubjectById(subjectId);
        }

        public List<Subject> getSubjectsForProfessor(int prof_if) {
            return subjectsDAO.getSubjectsByProfID(prof_if);
        }

        public List<Subject> getSubjectsWithoutProfessor()
        {
            return subjectsDAO.findAllWithoutProfessor();
        }

        public List<Subject> GetSubjectsPage(int page, int itemsPerPage)
        {

            List<Subject> allSubjects = GetAllSubjects();


            int startIndex = page * itemsPerPage;


            var SubjectsPage = allSubjects.Skip(startIndex).Take(itemsPerPage).ToList();

            return SubjectsPage;
        }

    }
}
