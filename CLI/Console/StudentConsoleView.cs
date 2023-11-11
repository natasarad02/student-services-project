using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
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
        string header = $"INDEX NUMBER {"",10} | NAME {"",21} | BIRTH DATE {"", 12} | ADDRESS{"",25} | PHONE NUMBER {"",12} | E-MAIL {"",30} | COLLEGE YEAR {"",4} | STATUS {"",10} | AVERAGE GRADE {"",4} |";
        
        System.Console.WriteLine(header);
        foreach(Student s in students)
        {
            System.Console.WriteLine(s);
        }
    }

    private Student InputStudent()
    {
        Model.Index indNum = new Model.Index();
        System.Console.WriteLine("Enter student's index number: ");
        string ind_num = System.Console.ReadLine() ?? string.Empty;
        indNum.FromString(ind_num);


        System.Console.WriteLine("Enter student name: ");
        string name  = System.Console.ReadLine() ?? string.Empty;
        string[] parts_of_name = name.Split(" ");
        string fName = parts_of_name[0];
        string lName = parts_of_name[1];

        System.Console.WriteLine("Enter student's date of birth: ");
        DateOnly br_date = DateOnly.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter student's street: ");
        string street = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter student's street number: ");
        int streetNum = int.Parse( System.Console.ReadLine() ?? string.Empty);
        System.Console.WriteLine("Enter student's city: ");
        string city = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter student's country: ");
        string country = System.Console.ReadLine() ?? string.Empty;
        Address adr = new Address(street, streetNum, city, country);


        System.Console.WriteLine("Enter student's phone number: ");
        int phone_num = int.Parse(System.Console.ReadLine() ?? string.Empty);

        System.Console.WriteLine("Enter student's e-mail: ");
        string email = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter student's year of college: ");
        int college_year = int.Parse(System.Console.ReadLine() ?? string.Empty);

        System.Console.WriteLine("Enter student's status: ");
        string stat = System.Console.ReadLine() ?? string.Empty;
        Status status;
        Enum.TryParse<Status>(stat, out status);

        System.Console.WriteLine("Enter student's average grade: ");
        float avg_grade = float.Parse(System.Console.ReadLine() ?? string.Empty);

        return new Student(lName, fName, br_date, adr, phone_num, email, indNum, college_year, status);
    }

    /*
    private int InputId()
    {
        System.Console.WriteLine("Enter student's id: ");
        return ConsoleViewUtils.SafeInputInt();
    }
    Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent
    */

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All students");
        System.Console.WriteLine("2: Add student");
        System.Console.WriteLine("3: Update student");
        System.Console.WriteLine("4: Remove student");
        System.Console.WriteLine("5: Show and sort students");
        System.Console.WriteLine("0: Close");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllStudents();
                break;
            case "2":
                AddStudent();
                break;
            case "3":
                UpdateStudent();
                break;
            case "4":
                RemoveStudent();
                break;
            case "5":
                ShowAndSortStudents();
                break;
        }
    }
    public void RunMenu()
    {
        while(true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0")
                break;
            HandleMenuInput(userInput);
        }
    }
}