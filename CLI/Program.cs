
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

    }
}