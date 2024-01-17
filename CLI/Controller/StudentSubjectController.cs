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

        public List<Subject> GetAllSubjectsByStudent(Student student, SubjectsController subjectsController)
        {
            List<int> subjectIds = new List<int>();

            List<StudentsSubjects> temp = GetAllStudentsSubjects().FindAll(s => s.studentID == student.ID);

            foreach (StudentsSubjects s in temp)
            {
                subjectIds.Add(s.subjectID);
            }

            List<Subject> subjects = new List<Subject>();

            foreach (int subjectId in subjectIds)
            {
                subjects.Add(subjectsController.GetSubjectById(subjectId));
            }

            return subjects;
        }

        public List<Student> GetStudents(int subj_id) {
            return studentsSubjectsDAO.GetStudents(subj_id);
        }

    }
}
