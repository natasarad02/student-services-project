using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GUI.View
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBox.Show("Usao u Convert");
            if (value is Status status)
            {
                if (status == Status.B)
                {
                    MessageBox.Show("Usao ovde u Budzet");
                    return "Government Budget";
                }
                else if (status == Status.S)
                {
                    MessageBox.Show("Usao ovde 1");
                    return "Self Financing";
                }
            }

            return null; // or default value if necessary
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string statusString)
            {
                if (statusString == "Government Budget")
                {
                    MessageBox.Show("Usao ovde u Budzet");
                    return Status.B;
                }
                else if (statusString == "Self Financing")
                {
                    MessageBox.Show("Usao ovde samof");
                    return Status.S;
                }
            }

            MessageBox.Show("ConvertBack not invoked for: " + value?.ToString()); // Debugging check

            return DependencyProperty.UnsetValue; // Indicates a failed conversion
        }

    }


}
