﻿using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
namespace StudentskaSluzba.Console;

class ProfessorConsoleView
{
    private readonly ProfessorDAO professorDAO;

    public ProfessorConsoleView(ProfessorDAO profDAO)
    {
        professorDAO = profDAO;
    }

    private void PrintProfessors(List<Professor> professors)
    {
        System.Console.WriteLine("Professors: ");
        string header = $"ID CARD NUMBER {"",10} | LAST NAME {"",10} | FIRST NAME {"", 11} | ADDRESS{"",25} | BIRTH DATE {"", 12} | PHONE NUMBER {"",12} | WORKING YEARS {"",2}| E-MAIL {"",30} | CALLING {"", 4} |";

        System.Console.WriteLine(header);
        foreach (Professor p in professors)
        {
            System.Console.WriteLine(p);
        }
    }

    private Professor InputProfessor()
    {

        int id = InputId();

        System.Console.WriteLine("Enter professor's ID card number: ");
        int idNum = int.Parse(System.Console.ReadLine() ?? string.Empty);
   


        System.Console.WriteLine("Enter professor's name: ");
        string name = System.Console.ReadLine() ?? string.Empty;
        string[] parts_of_name = name.Split(" ");
        string fName = parts_of_name[0];
        string lName = parts_of_name[1];

      

        System.Console.WriteLine("Enter professor's street: ");
        string street = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter professor's street number: ");
        int streetNum = int.Parse(System.Console.ReadLine() ?? string.Empty);
        System.Console.WriteLine("Enter professor's city: ");
        string city = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("Enter professor's country: ");
        string country = System.Console.ReadLine() ?? string.Empty;
        Address adr = new Address(street, streetNum, city, country);

        System.Console.WriteLine("Enter professor's date of birth: ");
        DateOnly br_date = DateOnly.Parse(System.Console.ReadLine());

        System.Console.WriteLine("Enter professor's phone number: ");
        string phone_num = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter professor's e-mail: ");
        string email = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Enter professor's year of employment: ");
        int employment_year = int.Parse(System.Console.ReadLine() ?? string.Empty);

        System.Console.WriteLine("Enter professor's calling: ");
        string call = System.Console.ReadLine() ?? string.Empty;

        return new Professor(id, idNum, fName, lName, adr, phone_num, br_date, employment_year, email, call);
    }


    private int InputId()
    {
        System.Console.WriteLine("Enter professor's id: ");
        // return ConsoleViewUtils.SafeInputInt();  Ovo cemo kasnije dodati i na slican nacin cemo primeniti SafeInput i za ostala polja u metodi InputStudent
        return int.Parse(System.Console.ReadLine());
    }


    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All professors");
        System.Console.WriteLine("2: Add professor");
        System.Console.WriteLine("3: Update professor");
        System.Console.WriteLine("4: Remove professor");
        System.Console.WriteLine("5: Show and sort professors");
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
                /*  case "5":
                      ShowAndSortProfessors();
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

    private void ShowAllProfessors()
    {
        PrintProfessors(professorDAO.GetAllProfessors());

    }

    private void RemoveProfessor()
    {
        int id = InputId();

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