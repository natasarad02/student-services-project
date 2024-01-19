using GUI.DTO;
using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using StudentskaSluzba.DAO;
using GUI.DTO;
using CLI.Controller;
using StudentskaSluzba.Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.View

{
    public partial class AddProfessor : Window, INotifyPropertyChanged
    {
        public ProfessorDTO Professor { get; set; }
        private ProfessorsController professorController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow mainWindow { get; set; }
        public AddProfessor(ProfessorsController professorController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Professor = new ProfessorDTO();
            this.professorController = professorController;
            this.mainWindow = mainWindow;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            Closing += Window_Closing;
            addButton.IsEnabled = false;
            Professor.PropertyChanged += Professor_PropertyChanged;
        }
        private void Professor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Professor.IsValid) { addButton.IsEnabled = true; }
            else
                addButton.IsEnabled = false;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            professorController.Add(Professor.ToProfessor());
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }

        private void txtStreet_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length == 0 && (e.Text == "+" || e.Text == ">" || e.Text == "<" || e.Text == "=" || char.IsPunctuation(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void txtStreet_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Validate if the field is required
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                txtStreetError.Text = "Required field";
            }
            // Validate if the input contains at least one non-numeric character
            else if (!textBox.Text.Any(c => char.IsLetter(c)))
            {
                txtStreetError.Text = "Input must contain at least one character";
            }
            // Validate string length
            else if (textBox.Text.Length >= 30 || textBox.Text.Length < 3)
            {
                txtStreetError.Text = "Street should be between 3 and 30 characters long.";
            }
            else
            {
                // Clear error message if no errors
                txtStreetError.Text = string.Empty;
            }
        }

        private void txtCity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length == 0 && (e.Text == "+" || e.Text == ">" || e.Text == "<" || e.Text == "=" || char.IsPunctuation(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
        /*
        private void txtCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Validate if the field is required
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                txtCityError.Text = "Required field";
            }
            // Validate if the input contains at least one non-numeric character
            else if (!textBox.Text.Any(c => char.IsLetter(c)))
            {
                txtCityError.Text = "Input must contain at least one character";
            }
            // Validate string length
            else if (textBox.Text.Length >= 30 || textBox.Text.Length < 3)
            {
                txtCityError.Text = "City should be between 3 and 30 characters long.";
            }
            else
            {
                // Clear error message if no errors
                txtCityError.Text = string.Empty;
            }
        }

        private void txtCountry_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length == 0 && (e.Text == "+" || e.Text == ">" || e.Text == "<" || e.Text == "=" || char.IsPunctuation(e.Text, 0)))
            {
                e.Handled = true;
            }
        }

        private void txtCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Validate if the field is required
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                txtCountryError.Text = "Required field";
            }
            // Validate if the input contains at least one non-numeric character
            else if (!textBox.Text.Any(c => char.IsLetter(c)))
            {
                txtCountryError.Text = "Input must contain at least one character";
            }
            // Validate string length
            else if (textBox.Text.Length >= 30 || textBox.Text.Length < 3)
            {
                txtCountryError.Text = "Country should be between 3 and 30 characters long.";
            }
            else
            {
                // Clear error message if no errors
                txtCountryError.Text = string.Empty;
            }
        }

        private void txtNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Validate input as integer with optional one to two letters at the end
            if (!Regex.IsMatch(textBox.Text + e.Text, @"^\d{1,4}[a-zA-Z]{0,2}$"))
            {
                e.Handled = true;
            }
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Validate if the field is required
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                txtNumberError.Text = "Required field";
            }
            // Validate if the input is in the correct format
            else if (!Regex.IsMatch(textBox.Text, @"^\d{1,4}[a-zA-Z]{0,2}$"))
            {
                txtNumberError.Text = "Invalid format. It should be an integer with optional one to two letters.";
            }
            else
            {
                // Clear error message if no errors
                txtNumberError.Text = string.Empty;
            }
        }


        */


    }

}
