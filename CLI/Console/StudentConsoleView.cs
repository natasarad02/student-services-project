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
        string header = $"INDEX NUMBER {"",10} | LAST NAME {"",10} | FIRST NAME {"", 11} | BIRTH DATE {"", 12} | ADDRESS{"",25} | PHONE NUMBER {"",12} | E-MAIL {"",30} | COLLEGE YEAR {"",4} | STATUS {"",10} | AVERAGE GRADE {"",4} |";
        
        //System.Console.WriteLine(header);
        foreach(Student s in students)
        {
            System.Console.WriteLine(s);
        }
    }

    private Student InputStudent()
    {
        Model.Index indNum = new Model.Index();
        System.Console.WriteLine("Enter student's index number: ");
        string indn = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        Model.Index ind_num = Model.Index.FromString(indn);


        System.Console.WriteLine("Enter student name: "); //podeliti na ime i prezime BITNO
        string name = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        string[] parts_of_name = name.Split(" ");
        string fName = parts_of_name[0];
        string lName = parts_of_name[1];

        System.Console.WriteLine("Enter student's date of birth: ");
        DateOnly br_date = ConsoleViewUtils.SafeInputDate(); //DateOnly.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter student's street: ");
        string street = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter student's street number: ");
        int streetNum = ConsoleViewUtils.SafeInputInt(); //int.Parse( System.Console.ReadLine() ?? string.Empty);
        System.Console.WriteLine("Enter student's city: ");
        string city = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter student's country: ");
        string country = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        Address adr = new Address(street, streetNum, city, country);


        System.Console.WriteLine("Enter student's phone number: ");
        int phone_num = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine() ?? string.Empty);

        System.Console.WriteLine("Enter student's e-mail: ");
        string email = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter student's year of college: ");
        int college_year = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine() ?? string.Empty);

        System.Console.WriteLine("Enter student's status: ");
        string stat = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        Status status;
        Enum.TryParse<Status>(stat, out status);

       // System.Console.WriteLine("Enter student's average grade: ");
        //float avg_grade = ConsoleViewUtils.SafeInputFloat(); //float.Parse(System.Console.ReadLine() ?? string.Empty);

        return new Student(lName, fName, br_date, adr, phone_num, email, indNum, college_year, status);
    }

    
    private int InputId()
    {
        System.Console.WriteLine("Enter student's id: ");
        return ConsoleViewUtils.SafeInputInt(); // Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent
        //return int.Parse(System.Console.ReadLine());
    }
   

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All students");
        System.Console.WriteLine("2: Add student");
        System.Console.WriteLine("3: Update student");
        System.Console.WriteLine("4: Remove student");
        System.Console.WriteLine("5: Show subjects for specific student");
        System.Console.WriteLine("6: Add subjects that student attends");
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
                System.Console.WriteLine("Enter student ID: ");
                int stud_ID = int.Parse(System.Console.ReadLine());
                List<Subject> temp = studentDAO.GetSubjects(stud_ID);
                System.Console.WriteLine(temp);
                break;
            case "6":
                System.Console.WriteLine("Enter students ID: ");
                int id = int.Parse(System.Console.ReadLine());
                //provera da li postoji BITNO
                System.Console.WriteLine("Enter subjects ID: ");
                int sub_id = int.Parse(System.Console.ReadLine());
                //dodati proveru da li postoji ta veza u ExamGrade BITNO
                studentDAO.addStudentSubject(id, sub_id);
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

    private void ShowAllStudents()
    {
        PrintStudents(studentDAO.GetAllStudents());

    }

    private void RemoveStudent()
    {
        int id = InputId();
       
        Student? removedStudent = studentDAO.removeStudent(id);
        if (removedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;


        }
        System.Console.WriteLine("Student is removed");
    }

    private void UpdateStudent()
    {
       int id = InputId();
       Student student = InputStudent();
       student.ID = id;
       Student? updatedStudent = studentDAO.UpdateStudent(student);
       if (updatedStudent == null) 
       {
            System.Console.WriteLine("Student not found");
            return;
        
        
       }
       System.Console.WriteLine("Student is updated");
    }

    private void AddStudent()
    {
        Student student = InputStudent();
        studentDAO.addStudent(student);
        System.Console.WriteLine("Student is added");
    }

   /* private void ShowAndSortStudents()
    {
        System.Console.WriteLine("\nEnter page: ");
        int page = ConsoleViewUtils.SafeInputInt();
        System.Console.WriteLine("\nEnter page size: ");
        int pageSize = ConsoleViewUtils.SafeInputInt();
        System.Console.WriteLine("\nEnter sort criteria: ");
        string sortCriteria = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("\nEnter 0 for ascending, any key for descending: ");
        int sortDirectionInput = ConsoleViewUtils.SafeInputInt();
        SortDirection sortDirection = sortDirectionInput == 0 ? SortDirection.Ascending : SortDirection.Descending;

        PrintVehicles(_vehiclesDao.GetAllVehicles(page, pageSize, sortCriteria, sortDirection));
    }
    Ovo cemo kad dodamo ConsoleViewUtils
    */
}