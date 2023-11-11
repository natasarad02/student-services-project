using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Console;

class ExamGradeConsoleView
{
    private readonly ExamGradeDAO examGradeDAO;

    public ExamGradeConsoleView(ExamGradeDAO examDAO)
    {
        examGradeDAO = examDAO;
    }

    private void PrintExamGrades(List<ExamGrade> examGrades)
    {
        System.Console.WriteLine("Grades: ");
        string header = $"STUDENT ID {"",4} | SUBJECT ID {"",2} | GRADE {"",2} | EXAM DATE {"",12}|";

        System.Console.WriteLine(header);
        foreach (ExamGrade eg in examGrades)
        {
            System.Console.WriteLine(eg);
        }
    }

    private ExamGrade InputExamGrade()
    {
        System.Console.WriteLine("Enter student's id: ");
        int stud_id = int.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter subject id: ");
        int sub_id = int.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter grade: ");
        int grade = int.Parse(System.Console.ReadLine());


        System.Console.WriteLine("Enter exam date: ");
        DateOnly exam_date = DateOnly.Parse(System.Console.ReadLine());

      

        return new ExamGrade(stud_id, sub_id, grade, exam_date);
    }


    private int InputId()
    {
        System.Console.WriteLine("Enter exam grade id: ");
        // return ConsoleViewUtils.SafeInputInt();  Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent
        return int.Parse(System.Console.ReadLine());
    }


    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All exam grades");
        System.Console.WriteLine("2: Add exam grade");
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
                /*  case "5":
                      ShowAndSortStudents();
                      break;*/
        }
    }
    public void RunMenu()
    {
        while (true)
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