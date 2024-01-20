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
    public class StudentsController
    {
        private readonly StudentDAO studentDAO;
      
        public StudentsController()
        {
            studentDAO = new StudentDAO();
           
        }

        public List<Student> GetAllStudents()
        {
            return studentDAO.GetAllStudents();
        }

        public void Add(Student student)
        {
            studentDAO.addStudent(student);
        }

        public void Delete(int studentId)
        {
            studentDAO.removeStudent(studentId);
        }

        public void Subscribe(IObserver observer)
        {
            studentDAO.StudentSubject.Subscribe(observer);
        }

        public void Update(Student student)
        {
            studentDAO.UpdateStudent(student);
        }

        public void Save() {
            studentDAO.save();
        }

        public List<Student> GetStudentsPage(int page, int itemsPerPage)
        {
            
            List<Student> allStudents = GetAllStudents();

            
            int startIndex = page * itemsPerPage;

            
            var studentsPage = allStudents.Skip(startIndex).Take(itemsPerPage).ToList();

            return studentsPage;
        }

        public List<Student> GetStudentsPageSearch(int page, int itemsPerPage, List<Student> searchResults)
        {

            List<Student> allStudents = searchResults;


            int startIndex = page * itemsPerPage;


            var studentsPage = allStudents.Skip(startIndex).Take(itemsPerPage).ToList();

            return studentsPage;
        }

        public float average_grade(int students_ID, ExamGradesController examGradesController) {
            
            List<ExamGrade> Passed_Exams = GetExamGradesByStudent(students_ID, examGradesController);

            float i = 0;
            float sum = 0;
            foreach (ExamGrade e in Passed_Exams)
            {
                sum += e.grade;
                i++;
            }
            return sum / i; 

        }


        public List<Student> GetSortedStudents(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            return studentDAO.sortedStudent(page, pageSize, sortCriteria, sortDirection);
        }

        public List<ExamGrade> GetExamGradesByStudent(int studentID, ExamGradesController examGradesController)
        {
            List<ExamGrade> rez = new List<ExamGrade>();
            foreach(ExamGrade grade in examGradesController.GetAllExamGrades())
            {
                if(grade.studentID == studentID)
                {
                    rez.Add(grade);
                }
            }
            return rez;

        }
    }
}

