using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace GUI.DateOnlyExtensions;
public static class DateOnlyExtensions
{
    public static DateTime ToDateTime(this DateOnly dateOnly)
    {
        
        return new DateTime(dateOnly.Day, dateOnly.Month, dateOnly.Year);
    }
}

