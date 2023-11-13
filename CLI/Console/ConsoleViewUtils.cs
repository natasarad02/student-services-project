using System;
using System.Collections.Generic;
using System.Linq;
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
}