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
    class SubjectDAO
    {
        private readonly List<Subject> subjects;
        private readonly Storage<Subject> storage;

        public SubjectDAO(string filename)
        {
            storage = new Storage<Subject>(filename + ".txt");
            subjects = storage.Load();
        }

        private int GenerateId()
        {
            if (subjects.Count == 0)
                return 0;
            return subjects[^1].Id + 1;
        }

        public Subject AddSubject(Subject subject)
        {
            subject.Id = GenerateId();
            subjects.Add(subject);
            storage.Save(subjects);
            return subject;
        }

        public Subject? UpdateSubject(Subject subject)
        {
            Subject? oldSubject = GetSubjectById(subject.Id);
            if (oldSubject is null) return null;

            oldSubject.Ids = subject.Ids;
            oldSubject.Name = subject.Name;
            oldSubject.semester = subject.semester;
            oldSubject.year = subject.year;
            oldSubject.ProfessorID = subject.ProfessorID;
            oldSubject.Espb = subject.Espb;
            oldSubject.Students_passed = subject.Students_passed;
            oldSubject.Students_attending = subject.Students_attending;

            storage.Save(subjects);
            return oldSubject;
        }

        public Subject? RemoveSubject(int id)
        {
            Subject? oldSubject = GetSubjectById(id);
            if (oldSubject is null) return null;

            subjects.Remove(oldSubject);
            storage.Save(subjects);
            return oldSubject;
        }

        public Subject? GetSubjectById(int id)
        {
            return subjects.Find(s => s.Id == id);
        }

        public List<Subject> GetAllSubjects()
        {
            return subjects;
        }
    }
}
