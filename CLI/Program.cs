
using StudentskaSluzba.Console;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
namespace StudentskaSluzba;

class Program
{
    static void Main()
    {
        StudentDAO students = new StudentDAO();
        StudentConsoleView studentView = new StudentConsoleView(students);
        studentView.RunMenu();

        ProfessorDAO professors = new ProfessorDAO();
        ProfessorConsoleView professorView = new ProfessorConsoleView(professors);
        professorView.RunMenu();

        SubjectDAO subjects = new SubjectDAO();
        SubjectConsoleView subjectView = new SubjectConsoleView(subjects);
        subjectView.RunMenu();

     
        ExamGradeDAO grades = new ExamGradeDAO();
        ExamGradeConsoleView gradesView = new ExamGradeConsoleView(grades);
        gradesView.RunMenu();

        System.Console.WriteLine("-------------------------------");
        /*List<int> students_1 = grades.GetStudentBySubject(1);
        foreach(int s in students_1)
        {
            System.Console.WriteLine(s);
        }*/

        List<Subject> students_2 = subjects.getSubjectsByProfID(0);
        foreach( Subject subject in students_2)
        {
            System.Console.WriteLine(subject);
        }

        DepartmentDAO departments = new DepartmentDAO();
        DepartmentConsoleView departmentView = new DepartmentConsoleView(departments);
        departmentView.RunMenu();
      
    

        StudentsSubjectsDAO stud_sub = new StudentsSubjectsDAO();
        
    }
}