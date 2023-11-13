
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


    }
}