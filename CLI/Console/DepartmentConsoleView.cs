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
            int departmentId = int.Parse(System.Console.ReadLine());

            System.Console.WriteLine("Enter department name: ");
            string departmentName = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Enter head of department: ");
            string headOfDepartment = System.Console.ReadLine() ?? string.Empty;

            return new Department(departmentId, departmentName, headOfDepartment);
        }


        private int InputId()
        {
            System.Console.WriteLine("Enter department's id: ");
            return int.Parse(System.Console.ReadLine());
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All departments");
            System.Console.WriteLine("2: Add department");
            System.Console.WriteLine("3: Update department");
            System.Console.WriteLine("4: Remove department");
            // Add more options as needed
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
