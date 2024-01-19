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
    }
    
}
