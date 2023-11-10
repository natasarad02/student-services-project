using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.DAO
{
    class ProfessorDAO
    {
        private readonly List<Professor> professors;
        private readonly Storage<Professor> storage;

        public ProfessorDAO(string filename)
        {
            storage = new Storage<Professor>(filename + ".txt");
            professors = storage.Load();
        }

        private int GenerateId()
        {
            if (professors.Count == 0)
                return 0;
            return professors[^1].Id + 1;
        }

        public Professor AddProfessor(Professor professor)
        {
            professor.Id = GenerateId();
            professors.Add(professor);
            storage.Save(professors);
            return professor;
        }

        public Professor? UpdateProfessor(Professor professor)
        {
            Professor? oldProfessor = GetProfessorById(professor.Id);
            if (oldProfessor is null) return null;

            // add updates

            storage.Save(professors);
            return oldProfessor;
        }

        public Professor? RemoveProfessor(int id)
        {
            Professor? oldProfessor = GetProfessorById(id);
            if (oldProfessor is null) return null;

            professors.Remove(oldProfessor);
            storage.Save(professors);
            return oldProfessor;
        }

        public Professor? GetProfessorById(int id)
        {
            return professors.Find(p => p.Id == id);
        }

        public List<Professor> GetAllProfessors()
        {
            return professors;
        }
    }
}
