using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View
{
    public partial class SubjectWarning : Window
    {

        public SubjectWarning()
        {
            InitializeComponent();
        }

        public void OK_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    
    }
}
