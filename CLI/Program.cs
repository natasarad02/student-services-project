
using StudentskaSluzba.Console;
using StudentskaSluzba.DAO;

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

        DepartmentDAO departments = new DepartmentDAO();
        DepartmentConsoleView departmentView = new DepartmentConsoleView(departments);
        departmentView.RunMenu();
    }
}