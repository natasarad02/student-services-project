using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentskaSluzba.Console;

static class ConsoleViewUtils
{
    public static int SafeInputInt()
    {
        int input;
        string raw = System.Console.ReadLine() ?? string.Empty;

        while(!int.TryParse(raw, out input))
        {
            System.Console.WriteLine("Not a valid number, try again: ");
            raw = System.Console.ReadLine();
        }

        return input;
    }

    public static float SafeInputFloat()
    {
        float input;
        string raw = System.Console.ReadLine() ?? string.Empty;

        while (!float.TryParse(raw, out input))
        {
            System.Console.WriteLine("Not a valid decimal number, try again: ");
            raw = System.Console.ReadLine();
        }

        return input;
    }


    public static string SafeInputString()
    {
        string input;
        input = System.Console.ReadLine();
        while(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
        {
            System.Console.WriteLine("Entry is empty, try again: ");
            input = System.Console.ReadLine();

        }
        return input;
    }

    public static string SafeInputName()
    {
        string input;
        input = System.Console.ReadLine();

        while (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input) || ContainsSpecialCharacters(input) || IsNumeric(input))
        {
            if (IsNumeric(input))
            {
                System.Console.WriteLine("Input is a number. Please enter a valid input: ");
            }
            else if (ContainsSpecialCharacters(input))
            {
                System.Console.WriteLine("Input contains special characters. Please enter a valid input: ");
            }
            else
            {
                System.Console.WriteLine("Entry is empty, try again: ");
            }

            input = System.Console.ReadLine();
        }

        return input;
    }

    
    private static bool ContainsSpecialCharacters(string input)
    {
        return !System.Text.RegularExpressions.Regex.IsMatch(input, @"^[a-zA-Z0-9 ]+$");
    }

    
    private static bool IsNumeric(string input)
    {
        return double.TryParse(input, out _);
    }




    public static DateOnly SafeInputDate()
    {
        DateOnly input;
        string raw = System.Console.ReadLine() ?? string.Empty;

        while (!DateOnly.TryParse(raw, out input))
        {
            System.Console.WriteLine("Not a valid date, please enter date in this format MM-DD-YYYY: ");
            raw = System.Console.ReadLine();
        }

        return input;

    }

    public static Status SafeInputStatus()
    {
        Status input;
        string stat = System.Console.ReadLine() ?? string.Empty;
        

        while(!Enum.TryParse<Status>(stat, out input))
        {
            System.Console.WriteLine("Not a valid status, enter 'S' or 'B'");
            stat = System.Console.ReadLine();
        }
        ;
        return input;

    }

    public static semester SafeInputSemester()
    {
        semester input;
        string sem = System.Console.ReadLine() ?? string.Empty;


        while(!Enum.TryParse<semester>(sem, out input))
        {
            System.Console.WriteLine("Not a valid semester, enter 'winter' or 'summer'");
            sem = System.Console.ReadLine();
        }
    ;
        return input;

    }

    public static string SafeInputPhoneNumber()
    {
        string input = System.Console.ReadLine();
        string reg_pattern = @"06[0-9]\/[0-9]{6,6}[0-9]?";
        Regex rg = new Regex(reg_pattern);
        while(!Regex.Match(input, reg_pattern).Success)
        {
            System.Console.WriteLine("Phone number isn't in the right format (06x/xxxxxxx), try again: ");
            input = System.Console.ReadLine();
        }
        return input;
    }

    public static string SafeInputIndex()
    {
        string input = System.Console.ReadLine();
        string reg_pattern = @"[a-zA-Z]{2,3}-\d{2,3}-\d{4}";
        Regex rg = new Regex(reg_pattern);
        while(!Regex.Match(input, reg_pattern).Success)
        {
            System.Console.WriteLine("Index isn't in the right format (CM - NUM - YEAR), try again:");
            input = System.Console.ReadLine();
        }
        return input;
    }

    public static string SafeInputEmail()
    {
        string input = System.Console.ReadLine();
        string reg_pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
        Regex rg = new Regex(reg_pattern);
        while(!Regex.Match(input, reg_pattern).Success)
        {
            System.Console.WriteLine("E-Mail isn't in the right format, try again:");
            input = System.Console.ReadLine();
        }
        return input;
    }

    public static int SafeInputGrade()
    {
        int grade;
        bool isValidGrade = false;

        do
        {
            System.Console.WriteLine("Enter the grade (between 6 and 10): ");
            string input = System.Console.ReadLine();

            if (int.TryParse(input, out grade))
            {
                if (grade >= 6 && grade <= 10)
                {
                    isValidGrade = true;
                }
                else
                {
                    System.Console.WriteLine("Grade should be between 6 and 10. Try again.");
                }
            }
            else
            {
                System.Console.WriteLine("Invalid input. Please enter a valid grade (between 6 and 10).");
            }

        } while (!isValidGrade);

        return grade;
    }

}