using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.DAO;

namespace CLI.Controller
{
    public class ProfessorsController
    {
        private readonly ProfessorDAO professors;

        public ProfessorsController()
        {
            professors = new ProfessorDAO();

        }

        public List<Professor> GetAllProfessors()
        {
            List<Professor> tmp = new List<Professor>();
            tmp = professors.GetAllProfessors();
            return tmp;
        }
    }
}
