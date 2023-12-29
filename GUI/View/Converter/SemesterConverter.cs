using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GUI.View
{
    public class SemesterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string selectedSemester = value as string;

            if (selectedSemester != null)
            {
                if (selectedSemester.Equals("Winter"))
                {
                    return "winter";
                }
                else if (selectedSemester.Equals("Summer"))
                {
                    return "summer";
                }
            }

          
            

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string selectedSemester = value as string;
            if (selectedSemester != null)
            {
                if (selectedSemester.Equals("winter"))
                {
                    return "Winter";
                }
                else if (selectedSemester.Equals("summer"))
                {
                    return "Summer";
                }

            }





            return null;
        }
    }
}
