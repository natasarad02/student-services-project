using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;

namespace StudentskaSluzba.Console
{
    class DepartmentConsoleView
    {
        private readonly DepartmentDAO departmentDAO;

        public DepartmentConsoleView(DepartmentDAO depDAO)
        {
            departmentDAO = depDAO;
        }

        private void PrintDepartments(List<Department> departments)
        {
            System.Console.WriteLine("Departments: ");
            string header = $"{"DepartmentId",-15}{"DepartmentName",-30}"; 
            // Add appropriate headers

            System.Console.WriteLine(header);
            foreach (Department d in departments)
            {
                System.Console.WriteLine(d);
            }
        }

        private Department InputDepartment()
        {
            System.Console.WriteLine("Enter department details:");

            System.Console.WriteLine("Enter department id: ");
            int departmentId = ConsoleViewUtils.SafeInputInt(); //int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Enter department name: ");
            string departmentName = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Enter head of department: ");
            string headOfDepartment = ConsoleViewUtils.SafeInputString(); //System.Console.ReadLine() ?? string.Empty;

            return new Department(departmentId, departmentName, headOfDepartment);
        }


        private int InputId()
        {
            System.Console.WriteLine("Enter department's id: ");
            return ConsoleViewUtils.SafeInputInt();
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All departments");
            System.Console.WriteLine("2: Add department");
            System.Console.WriteLine("3: Update department");
            System.Console.WriteLine("4: Remove department");
            System.Console.WriteLine("5: Add professors");
            System.Console.WriteLine("6: List professors");
            System.Console.WriteLine("0: Close");
        }

        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllDepartments();
                    break;
                case "2":
                    AddDepartment();
                    break;
                case "3":
                    UpdateDepartment();
                    break;
                case "4":
                    RemoveDepartment();
                    break;
                case "5":
                    System.Console.WriteLine("Enter department ID: ");
                    int id = int.Parse(System.Console.ReadLine());
                    System.Console.WriteLine("Enter professors ID: ");
                    int id_p = int.Parse(System.Console.ReadLine());
                    departmentDAO.addProfessor(id, id_p);
                    break;
                case "6":
                    System.Console.WriteLine("Enter department ID: ");
                    int id_d = int.Parse(System.Console.ReadLine());
                    List<Professor> profs= departmentDAO.GetDepartmentById(id_d).Department_Professors;
                    foreach (Professor prof in profs)
                    {
                        System.Console.WriteLine(prof.calling+" "+ prof.Name+" "+prof.Surname);
                    }
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

        private void ShowAllDepartments()
        {
            PrintDepartments(departmentDAO.GetAllDepartments());
        }

        private void RemoveDepartment()
        {
            int id = InputId();

            Department? removedDepartment = departmentDAO.RemoveDepartment(id);
            if (removedDepartment == null)
            {
                System.Console.WriteLine("Department not found");
                return;
            }

            System.Console.WriteLine("Department is removed");
        }

        private void UpdateDepartment()
        {
            int id = InputId();
            Department department = InputDepartment();
            department.Id = id;
            Department? updatedDepartment = departmentDAO.UpdateDepartment(department);
            if (updatedDepartment == null)
            {
                System.Console.WriteLine("Department not found");
                return;
            }

            System.Console.WriteLine("Department is updated");
        }

        private void AddDepartment()
        {
            Department department = InputDepartment();
            departmentDAO.AddDepartment(department);
            System.Console.WriteLine("Department is added");
        }
    }
}
