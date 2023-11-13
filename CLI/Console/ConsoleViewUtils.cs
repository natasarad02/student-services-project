using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
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

    public static DateOnly SafeInputDate()
    {
        DateOnly input;
        string raw = System.Console.ReadLine() ?? string.Empty;

        while (!DateOnly.TryParse(raw, out input))
        {
            System.Console.WriteLine("Not a valid date, try again: ");
            raw = System.Console.ReadLine();
        }

        return input;

    }
}