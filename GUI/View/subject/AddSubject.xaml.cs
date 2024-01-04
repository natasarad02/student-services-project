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
    public partial class AddSubject : Window, INotifyPropertyChanged
    {
        public SubjectDTO Subject { get; set; }
        private SubjectsController subjectController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddSubject(SubjectsController subjectController)
        {
            InitializeComponent();
            DataContext = this;
            Subject = new SubjectDTO();
            this.subjectController = subjectController;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            subjectController.Add(Subject.ToSubject());
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Professor(object sender, RoutedEventArgs e)
        {
            ProfessorList professorList = new ProfessorList(Subject, subjectController);
            professorList.Show();
        }
    }

}
