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
        
        foreach (ExamGrade eg in examGrades)
        {
            System.Console.WriteLine(eg);
        }
    }

    private ExamGrade InputExamGrade()
    {
        System.Console.WriteLine("Enter student's id: ");
        int stud_id = ConsoleViewUtils.SafeInputInt(); 

        System.Console.WriteLine("Enter subject id: ");
        int sub_id = ConsoleViewUtils.SafeInputInt(); 

        System.Console.WriteLine("Enter grade: ");
        int grade = ConsoleViewUtils.SafeInputGrade();


        System.Console.WriteLine("Enter exam date: ");
        DateTime exam_date = ConsoleViewUtils.SafeInputDate(); 



        return new ExamGrade(stud_id, sub_id, grade, exam_date);
    }


    private int InputId()
    {
        System.Console.WriteLine("Enter exam grade id: ");
        return ConsoleViewUtils.SafeInputInt();  //Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent
        //return int.Parse(System.Console.ReadLine());
    }


    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All exam grades");
        System.Console.WriteLine("2: Add exam grade");
        System.Console.WriteLine("3: Update exam grade");
        System.Console.WriteLine("4: Remove exam grade");
        System.Console.WriteLine("0: Close");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllExamGrades();
                break;
            case "2":
                AddExamGrade();
                break;
            case "3":
                UpdateExamGrade();
                break;
            case "4":
                RemoveExamGrade();
                break;
                
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

    private void ShowAllExamGrades()
    {
        PrintExamGrades(examGradeDAO.GetAllExamGrades());

    }

    private void RemoveExamGrade()
    {
        int id = InputId();
        while (!examGradeDAO.doesGradeExist(id))
        {
            System.Console.WriteLine("Grade doesn't exist, try again: ");
            System.Console.WriteLine("Enter exam grade ID: ");
            id = int.Parse(System.Console.ReadLine());
        }
        ExamGrade? removedExamGrade = examGradeDAO.RemoveExamGrade(id);
        if (removedExamGrade == null)
        {
            System.Console.WriteLine("Exam grade not found");
            return;


        }
        System.Console.WriteLine("Exam grade is removed");
    }

    private void UpdateExamGrade()
    {
        int id = InputId();
        while (!examGradeDAO.doesGradeExist(id))
        {
            System.Console.WriteLine("Grade doesn't exist");
            return;

        }
        ExamGrade exam_grade = InputExamGrade();
        exam_grade.ID = id;
        ExamGrade? updatedExamGrade = examGradeDAO.UpdateExamGrade(exam_grade);
        if (updatedExamGrade == null)
        {
            System.Console.WriteLine("Exam grade not found");
            return;


        }
        System.Console.WriteLine("Exam grade is updated");
    }

    private void AddExamGrade()
    {
        ExamGrade exam_grade = InputExamGrade();
        examGradeDAO.AddExamGrade(exam_grade);
        System.Console.WriteLine("Exam grade is added");
    }

    
}