
using CLI.Model.DAO;
using StudentskaSluzba.Console;
using StudentskaSluzba.Model;
namespace StudentskaSluzba;

class Program
{
    static void Main()
    {
        while (true)
        {
            // Start Menu
            System.Console.WriteLine("\nSTART MENU: ");
            System.Console.WriteLine("1: Use Student entity");
            System.Console.WriteLine("2: Use Professor entity");
            System.Console.WriteLine("3: Use Subject entity");
            System.Console.WriteLine("4: Use Exam Grade entity");
            System.Console.WriteLine("5: Use Department entity");
            System.Console.WriteLine("0: Close");

            string startInput = System.Console.ReadLine() ?? "0";

            if (startInput == "0")
            {
                break;
            }
            switch (startInput)
            {
                case "1":
                    StudentDAO students = new StudentDAO();
                    StudentConsoleView studentView = new StudentConsoleView(students);
                    studentView.RunMenu();
                    break;
                case "2":
                    ProfessorDAO professors = new ProfessorDAO();
                    ProfessorConsoleView professorView = new ProfessorConsoleView(professors);
                    professorView.RunMenu();
                    break;
                case "3":
                    SubjectDAO subjects = new SubjectDAO();
                    SubjectConsoleView subjectView = new SubjectConsoleView(subjects);
                    subjectView.RunMenu();
                    break;
                case "4":
                    ExamGradeDAO grades = new ExamGradeDAO();
                    ExamGradeConsoleView gradesView = new ExamGradeConsoleView(grades);
                    gradesView.RunMenu();
                    break;
                case "5":
                    DepartmentDAO departments = new DepartmentDAO();
                    DepartmentConsoleView departmentView = new DepartmentConsoleView(departments);
                    departmentView.RunMenu();
                    break;




            }

        }

        System.Console.WriteLine("------------------------------- END OF PROGRAM -------------------------------");





    }
}