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
using System.Collections.ObjectModel;
using GUI.Localization;
using System.Windows.Input;

namespace GUI.View
{
    public partial class DeleteSubjectFromStudent : Window, INotifyPropertyChanged
    {
        public SubjectDTO SelectedSubject { get; set; }
        public StudentDTO Student { get; set; }
        private StudentsSubjectsController studentsSubjectController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SubjectDTO> Subjects{ get; set; }

        public UpdateStudent parentWindow { get; set; }
        public DeleteSubjectFromStudent(StudentsSubjectsController studentsSubjectController, ObservableCollection<SubjectDTO> Subjects, StudentDTO Student, SubjectDTO SelectedSubject, UpdateStudent parentWindow)
        {
            InitializeComponent();
            DataContext = this;
            this.SelectedSubject = SelectedSubject;
            this.studentsSubjectController = studentsSubjectController;
            this.Subjects = Subjects;
            this.Student = Student;
            this.parentWindow = parentWindow;
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
            parentWindow.IsEnabled = false;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            studentsSubjectController.Delete(Student.Id, SelectedSubject.Id);
            Subjects.Remove(SelectedSubject);
            parentWindow.IsEnabled = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.IsEnabled = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }
    }

}
