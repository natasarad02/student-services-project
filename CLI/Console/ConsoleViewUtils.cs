using StudentskaSluzba.Model;
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

    public static Status SafeInputStatus()
    {
        Status input;
        string stat = System.Console.ReadLine() ?? string.Empty;
        

        if(!Enum.TryParse<Status>(stat, out input))
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


        if (!Enum.TryParse<semester>(sem, out input))
        {
            System.Console.WriteLine("Not a valid semester, enter 'winter' or 'summer'");
            sem = System.Console.ReadLine();
        }
    ;
        return input;

    }
}