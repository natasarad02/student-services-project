using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Console;

class StudentConsoleView
{
    private readonly StudentDAO studentDAO;

    public StudentConsoleView(StudentDAO studDAO)
    {
        studentDAO = studDAO;
    }

    private void PrintStudents(List<Student> students)
    {
        System.Console.WriteLine("Students: ");
        string header = $"INDEX NUMBER {"",10} | NAME {"",21} | ADDRESS{"",25} | PHONE NUMBER {"",12} | E-MAIL {"",30} | COLLEGE YEAR {"",4} | STATUS {"",10} | AVERAGE GRADE {"",4} |";
        
        System.Console.WriteLine(header);
        foreach(Student s in students)
        {
            System.Console.WriteLine(s);
        }
    }

    
}