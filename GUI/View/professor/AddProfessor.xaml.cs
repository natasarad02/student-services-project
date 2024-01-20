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
using GUI.DTO;
using CLI.Controller;
using StudentskaSluzba.Model;
using System.Windows.Controls;
using System.Windows.Input;
using GUI.Localization;

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
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                Serbian_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                English_Click(sender, e);

        }
        public void English_Click(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        public void Serbian_Click(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-RS");
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
           
        }

        private void txtCity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Length == 0 && (e.Text == "+" || e.Text == ">" || e.Text == "<" || e.Text == "=" || char.IsPunctuation(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
       


    }

}
