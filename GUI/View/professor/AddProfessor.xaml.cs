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
namespace GUI.View

{
    public partial class AddProfessor : Window, INotifyPropertyChanged
    {
        public ProfessorDTO Professor { get; set; }
        private ProfessorDAO professorsDAO;
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddProfessor(ProfessorDAO professorsDAO)
        {
            InitializeComponent();
            DataContext = this;
            Professor = new ProfessorDTO();
            this.professorsDAO = professorsDAO;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            professorsDAO.AddProfessor(Professor.ToProfessor());
            Close();
        }
    }
    
}
