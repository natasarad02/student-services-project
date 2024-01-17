using CLI.Controller;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View
{
    public partial class DeleteStudent: Window, INotifyPropertyChanged 
    {
        public StudentDTO Student { get; set; }
        private StudentsController studentController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public DeleteStudent(StudentsController studentController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentController = studentController;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            studentController.Delete(Student.toStudent().ID);
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
