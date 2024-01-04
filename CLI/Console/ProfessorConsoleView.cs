using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Console;

class ProfessorConsoleView
{
    private readonly ProfessorDAO professorDAO = new ProfessorDAO();

    public ProfessorConsoleView(ProfessorDAO profDAO)
    {
        professorDAO = profDAO;
    }

    private void PrintProfessors(List<Professor> professors)
    {
        System.Console.WriteLine("Professors: ");
        
        foreach (Professor p in professors)
        {
            System.Console.WriteLine(p);
        }
    }

    private Professor InputProfessor()
    {

        System.Console.WriteLine("Enter professor's ID card number: ");
        int idNum = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine() ?? string.Empty);



        System.Console.WriteLine("Enter professor's last name: "); 
        string lName = ConsoleViewUtils.SafeInputName(); //System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter professor's first name: "); 
        string fName = ConsoleViewUtils.SafeInputName();




        System.Console.WriteLine("Enter professor's street: ");
        string street = ConsoleViewUtils.SafeInputName(); //System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter professor's street number: ");
        int streetNum = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine() ?? string.Empty);
        System.Console.WriteLine("Enter professor's city: ");
        string city = ConsoleViewUtils.SafeInputName(); //System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter professor's country: ");
        string country = ConsoleViewUtils.SafeInputName(); //System.Console.ReadLine() ?? string.Empty;
        Address adr = new Address(street, streetNum, city, country);

        System.Console.WriteLine("Enter professor's date of birth: ");
        DateTime br_date = ConsoleViewUtils.SafeInputDate();//DateOnly.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter professor's phone number: ");
        string phone_num = ConsoleViewUtils.SafeInputPhoneNumber(); //System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter professor's e-mail: ");
        string email = ConsoleViewUtils.SafeInputEmail(); //System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter professor's year of employment: ");
        int employment_year = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine() ?? string.Empty);

        System.Console.WriteLine("Enter professor's calling: ");
        string call = ConsoleViewUtils.SafeInputName(); //System.Console.ReadLine() ?? string.Empty;

        return new Professor( idNum, fName, lName, adr, phone_num, br_date, employment_year, email, call);
    }


    private int InputId()
    {
        System.Console.WriteLine("Enter professor's id: ");
        return ConsoleViewUtils.SafeInputInt();  //Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent
        //return int.Parse(System.Console.ReadLine());
    }


    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All professors");
        System.Console.WriteLine("2: Add professor");
        System.Console.WriteLine("3: Update professor");
        System.Console.WriteLine("4: Remove professor");
        System.Console.WriteLine("5: List subjects that specific professor teaches");
        System.Console.WriteLine("6: Show and sort professors");
        System.Console.WriteLine("0: Close");
    }

    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllProfessors();
                break;
            case "2":
                AddProfessor();
                break;
            case "3":
                UpdateProfessor();
                break;
            case "4":
                RemoveProfessor();
                break;
            case "5":
                System.Console.WriteLine("Enter professors ID: ");
                int id = int.Parse(System.Console.ReadLine());
                // BITNO proveri da li profesor postoji? --> odradjeno
                while (!professorDAO.doesProfessorExist(id))
                {
                    System.Console.WriteLine("Professor doesn't exist, try again: ");
                    System.Console.WriteLine("Enter professors ID: ");
                    id = int.Parse(System.Console.ReadLine());
                }
                List<Subject> temp = professorDAO.listSubjects(id);
                foreach(Subject subject in temp)
                {
                    System.Console.WriteLine(subject);
                }
                break;
            case "6":
                sort();
                break;
        }
    }

    public void sort()
    {

        System.Console.WriteLine("\nEnter page: ");
        int page = ConsoleViewUtils.SafeInputInt();
        System.Console.WriteLine("\nEnter page size: ");
        int pageSize = ConsoleViewUtils.SafeInputInt();
        System.Console.WriteLine("\nEnter sort criteria: ");
        System.Console.WriteLine("\nPossible sort criterias: Id, Name, Last name, E-Mail");
        string sortCriteria = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("\nEnter 0 for ascending, any key for descending: ");
        int sortDirectionInput = ConsoleViewUtils.SafeInputInt();
        SortDirection sortDirection = sortDirectionInput == 0 ? SortDirection.Ascending : SortDirection.Descending;

        List<Professor> sorted_professors = professorDAO.sortedProfessor(page, pageSize, sortCriteria, sortDirection);

        PrintProfessors(sorted_professors);

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

    private void ShowAllProfessors()
    {
        PrintProfessors(professorDAO.GetAllProfessors());

    }

    private void RemoveProfessor()
    {
        int id = InputId();
        while (!professorDAO.doesProfessorExist(id))
        {
            System.Console.WriteLine("Professor doesn't exist, try again: ");
            System.Console.WriteLine("Enter professor's ID: ");
            id = int.Parse(System.Console.ReadLine());
        }

        Professor? removedProfessor = professorDAO.RemoveProfessor(id);
        if (removedProfessor == null)
        {
            System.Console.WriteLine("Professor not found");
            return;


        }
        System.Console.WriteLine("Professor is removed");
    }

    private void UpdateProfessor()
    {
        int id = InputId();
        while (!professorDAO.doesProfessorExist(id))
        {
            System.Console.WriteLine("Professor doesn't exist");
            return;

        }
        Professor professor = InputProfessor();
        professor.Id = id;
        Professor? updatedProfessor = professorDAO.UpdateProfessor(professor);
        if (updatedProfessor == null)
        {
            System.Console.WriteLine("Professor not found");
            return;


        }
        System.Console.WriteLine("Professor is updated");
    }

    private void AddProfessor()
    {
        Professor professor = InputProfessor();
        professorDAO.AddProfessor(professor);
        System.Console.WriteLine("Professor is added");
    }

    /* private void ShowAndSortProfessors()
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