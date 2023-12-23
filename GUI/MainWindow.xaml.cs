using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Get the screen dimensions
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Calculate 3/4 of the screen size
            double targetWidth = screenWidth * 0.75;
            double targetHeight = screenHeight * 0.75;

            // Set the window size
            Width = targetWidth;
            Height = targetHeight;

            Left = (screenWidth - targetWidth) / 2;
            Top = (screenHeight - targetHeight) / 2;

        }

        private string SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                TabItem selectedTab = tabControl.SelectedItem as TabItem;

                if (selectedTab != null)
                {
                    string tabHeader = selectedTab.Header.ToString();


                    return tabHeader;
                    // Perform actions based on the selected tab
                    /*switch (tabHeader)
                    {
                        case "Students":
                            // Access and manipulate Student entities
                            // Example: viewModel.DeleteStudent();
                            break;
                        case "Subjects":
                            // Access and manipulate Subject entities
                            // Example: viewModel.AddSubject();
                            break;
                        case "Professors":
                            // Access and manipulate Professor entities
                            // Example: viewModel.EditProfessor();
                            break;
                        case "Departments":
                            // Access and manipulate Department entities
                            // Example: viewModel.UpdateDepartment();
                            break;
                            // Add more cases for additional tabs if needed
                    }*/
                }
            }

            return null;

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                TabItem selectedTab = tabControl.SelectedItem as TabItem;

                if (selectedTab != null)
                {
                    string tabHeader = selectedTab.Header.ToString();

                    // Update the TextBlock text to display the current tab
                    currentTabTextBlock.Text = "Current Tab: " + tabHeader;
                }
            }
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
