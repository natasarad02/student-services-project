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
    public partial class DeleteSubject : Window, INotifyPropertyChanged
    {
        public SubjectDTO Subject { get; set; }
        private SubjectsController subjectController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public DeleteSubject(SubjectsController subjectController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Subject = new SubjectDTO();
            this.subjectController = subjectController;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            subjectController.Delete(Subject.ToSubject().Id);
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
