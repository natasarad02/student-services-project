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
using CLI.Observer;

namespace GUI.View
{
    public partial class DeleteSubject : Window, INotifyPropertyChanged, IObserver
    {
        public SubjectDTO Subject { get; set; }
        private SubjectsController subjectController;
        private StudentsSubjectsController studentsSubjectsController;
        private ExamGradesController examGradesController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow mainWindow {  get; set; }
        public DeleteSubject(SubjectsController subjectController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Subject = new SubjectDTO();
            this.subjectController = subjectController;

            studentsSubjectsController = new StudentsSubjectsController();
            studentsSubjectsController.Subscribe(this);
            examGradesController = new ExamGradesController();
            examGradesController.Subscribe(this);
            DataContext = this;

            this.mainWindow = mainWindow;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            Closing += Window_Closing;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            bool a = true;
            bool b = true;
            //da li postoje Studenti koji pohadjaju?
            if(studentsSubjectsController.GetStudents(Subject.ToSubject().Id)==null)
            {
                a = false;
            }

            //da li postoje ocene?
            if(examGradesController.getGradesForSubject(Subject.ToSubject().Id) == null)
            {
                b = false;
            }

            if(a&& b)
            {
                subjectController.Delete(Subject.ToSubject().Id);
            }
            else
            {
                SubjectWarning subjectWarning = new SubjectWarning();
                subjectWarning.Show();
            }
            mainWindow.IsEnabled = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }

        public void Update()
        {
        }
    }

}
