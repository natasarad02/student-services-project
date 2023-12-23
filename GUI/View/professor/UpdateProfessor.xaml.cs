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
using CLI.Controller;
namespace GUI.View
{
    public partial class UpdateProfessor : Window, INotifyPropertyChanged
    {
        public ProfessorDTO Professor { get; set; }
        private ProfessorsController professorController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateProfessor(ProfessorsController professorController)
        {
            InitializeComponent();
            DataContext = this;
            Professor = new ProfessorDTO();
            this.professorController = professorController;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            professorController.UpdateProfessor(Professor.ToProfessor());
            Close();
        }
    }

}
