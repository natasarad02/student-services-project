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
namespace GUI.View

{
    public partial class AddProfessor : Window, INotifyPropertyChanged
    {
        public ProfessorDTO Professor { get; set; }
        private ProfessorsController professorController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddProfessor(ProfessorsController professorController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Professor = new ProfessorDTO();
            this.professorController = professorController;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            professorController.Add(Professor.ToProfessor());
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
}
