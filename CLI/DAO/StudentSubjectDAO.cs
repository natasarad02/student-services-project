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
    class StudentsSubjectsDAO
    {
        private readonly List<StudentsSubjects> StudentsSubjectss;
        private readonly Storage<StudentsSubjects> storage;
        private static StudentDAO studentDAO = new StudentDAO();
        private static SubjectDAO subjectDAO = new SubjectDAO();

        public StudentsSubjectsDAO()
        {
            storage = new Storage<StudentsSubjects>("StudentsSubjects" + ".txt");
            StudentsSubjectss = storage.Load();
        }

        public StudentsSubjects AddStudentsSubjects(StudentsSubjects StudentsSubjects)
        {
            //fali provera da li vec pohadja??? BITNO
            StudentsSubjectss.Add(StudentsSubjects);
            storage.Save(StudentsSubjectss);

            return StudentsSubjects;
        }

        public StudentsSubjects? RemoveStudentsSubjects(int id_s, int id_sub)
        {
            StudentsSubjects? oldStudentsSubjects = StudentsSubjectss.Find(s=>s.studentID==id_s && s.subjectID==id_sub);
            if (oldStudentsSubjects is null) return null;

            StudentsSubjectss.Remove(oldStudentsSubjects);
            storage.Save(StudentsSubjectss);
            return oldStudentsSubjects;
        }

        public List<Student> GetStudents(int id) //find all students that are taking this subject/class
        {

            List<int> studentIDs = new List<int>();

            List<StudentsSubjects> temp = StudentsSubjectss.FindAll(s => s.subjectID == id);

            foreach(StudentsSubjects s in temp)
            {
                studentIDs.Add(s.studentID);
            }

            List<Student> students = new List<Student>();

            foreach (int studentID in studentIDs)
            {
                students.Add(studentDAO.getStudentbyId(studentID));
            }

            return students;

        }

        public List<Subject> GetSubjects(int studentId) //find all subjects that one student takes
        {
            List<int> subjectIds = new List<int>();

            List<StudentsSubjects> temp = StudentsSubjectss.FindAll(s => s.studentID == studentId);

            foreach (StudentsSubjects s in temp)
            {
                subjectIds.Add(s.subjectID);
            }

            List<Subject> subjects = new List<Subject>();

            foreach (int subjectId in subjectIds)
            {
                subjects.Add(subjectDAO.GetSubjectById(subjectId));
            }

            return subjects;
        }


        public List<StudentsSubjects> GetAllStudentsSubjectss()
        {
            return StudentsSubjectss;
        }

        public bool doesConnectionExist(int stud_id, int sub_id)
        {
            StudentsSubjects connection = StudentsSubjectss.Find(s => s.subjectID == sub_id && s.studentID == stud_id);
            if (connection == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool doesConnectionBySubjectExist(int sub_id)
        {
            StudentsSubjects connection = StudentsSubjectss.Find(s => s.subjectID == sub_id);
            if (connection == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}