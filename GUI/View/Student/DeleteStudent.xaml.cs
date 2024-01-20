using CLI.Controller;
using GUI.DTO;
using GUI.Localization;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI.View
{
    public partial class DeleteStudent: Window, INotifyPropertyChanged 
    {
        public StudentDTO Student { get; set; }
        private StudentsController studentController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow mainWindow { get; set; }
        private ExamGradesController examGradesController;
        public DeleteStudent(StudentsController studentController, MainWindow mainWindow, ExamGradesController examGradesController)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentController = studentController;
            this.mainWindow = mainWindow;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            this.examGradesController = examGradesController;
            Closing += Window_Closing;
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
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            foreach(ExamGrade grade in studentController.GetExamGradesByStudent(Student.Id, examGradesController))
            {
                examGradesController.Delete(grade.ID);
                
            }
            studentController.Delete(Student.toStudent().ID);
            mainWindow.IsEnabled = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }
      
    }
}
