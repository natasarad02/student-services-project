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
        private SubjectDAO subjectDAO = new SubjectDAO();

        public ProfessorDAO()
        {
            storage = new Storage<Professor>("professors.txt");
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

            oldProfessor.Surname = professor.Surname;
            oldProfessor.Name = professor.Name;
            oldProfessor.birth_date = professor.birth_date;
            oldProfessor.Address = professor.Address;
            oldProfessor.phone_number = professor.phone_number;
            oldProfessor.email_address = professor.email_address;
            oldProfessor.num = professor.num;
            oldProfessor.calling = professor.calling;
            oldProfessor.work_year = professor.work_year;
            oldProfessor.Subjects = professor.Subjects;
            oldProfessor.employment_year = professor.employment_year;

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

        public List<Subject> listSubjects(int id_prof)
        {
            return subjectDAO.getSubjectsByProfID(id_prof);
        }

        
    }
}
