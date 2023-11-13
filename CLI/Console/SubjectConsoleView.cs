using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Console;

class SubjectConsoleView
{
    private readonly SubjectDAO subjectDAO;

    public SubjectConsoleView(SubjectDAO subDAO)
    {
        subjectDAO = subDAO;
    }

    private void PrintSubjects(List<Subject> subjects)
    {
        System.Console.WriteLine("Subjects: ");
        string header = $"?????????"; //dodati

        System.Console.WriteLine(header);
        foreach (Subject s in subjects)
        {
            System.Console.WriteLine(s);
        }
    }

    private Subject InputSubject()
    {
        System.Console.WriteLine("Enter subject details:");

        System.Console.WriteLine("Enter subject id: ");
        int id = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter subject name: ");
        string name = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter subject ESPB: ");
        int espb = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter subject semester: ");
        string semesterStr = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;
        semester semester;
        Enum.TryParse<semester>(semesterStr, out semester); //popraviti -> popravljeno

        System.Console.WriteLine("Enter subject school year: ");
        int schoolYear = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter subject professor id: ");
        int professorId = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine());

        return new Subject(id, name, espb, semester, schoolYear, professorId);
    }


    private int InputId()
    {
        System.Console.WriteLine("Enter subjects's id: ");
        return ConsoleViewUtils.SafeInputInt(); // Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent --> dodato
       // return int.Parse(System.Console.ReadLine());
    }


    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All subjects");
        System.Console.WriteLine("2: Add subject");
        System.Console.WriteLine("3: Update subject");
        System.Console.WriteLine("4: Remove subjects");
        System.Console.WriteLine("5: Show and sort subjects");
        System.Console.WriteLine("0: Close");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllSubjects();
                break;
            case "2":
                AddSubject();
                break;
            case "3":
                UpdateSubject();
                break;
            case "4":
                RemoveSubject();
                break;
                /*  case "5":
                      ShowAndSortSubjects();
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

    private void ShowAllSubjects()
    {
        PrintSubjects(subjectDAO.GetAllSubjects());

    }

    private void RemoveSubject()
    {
        int id = InputId();

        Subject? removedSubject = subjectDAO.RemoveSubject(id);
        if (removedSubject == null)
        {
            System.Console.WriteLine("Subject not found");
            return;


        }
        System.Console.WriteLine("Subject is removed");
    }

    private void UpdateSubject()
    {
        int id = InputId();
        Subject subject = InputSubject();
        subject.Id = id;
        Subject? updatedSubject = subjectDAO.UpdateSubject(subject);
        if (updatedSubject == null)
        {
            System.Console.WriteLine("Subject not found");
            return;


        }
        System.Console.WriteLine("Subject is updated");
    }

    private void AddSubject()
    {
        Subject subject = InputSubject();
        subjectDAO.AddSubject(subject);
        System.Console.WriteLine("Subject is added");
    }

    
}