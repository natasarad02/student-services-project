using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CLI.Controller;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;
using GUI.DTO;
using CLI.Observer;
using System.Collections.Generic;

namespace GUI.View
{
    public partial class StudentList : Window, IObserver
    {

        public ObservableCollection<Student> students { get; set; }

        public ProfessorDTO professor { get; set; }

        private StudentsSubjectsController studentsSubjectsController { get; set; }

        private SubjectsController subjectsController { get; set; }

        //List of students for a professor
        public UpdateProfessor parentWindow { get; set; }
        public StudentList(ProfessorDTO pprofessor, SubjectsController subjectsController, UpdateProfessor parentWindow) 
        {
            InitializeComponent();

            students = new ObservableCollection<Student>();
            professor = pprofessor;
            this.studentsSubjectsController = new StudentsSubjectsController();
            this.subjectsController = subjectsController;
            studentsSubjectsController.Subscribe(this);
            subjectsController.Subscribe(this);

            DataContext = this;
            Update();
            this.parentWindow = parentWindow;
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
            parentWindow.IsEnabled = false;
            Closing += Window_Closing;
        }

        //za svakog profesora naci sve predmete -> subjectController
        //za svaki predmet naci sve studente -> studentSubjectController
        //info o profesoru
        public void Update() {
            students.Clear();

            foreach (Subject subject in subjectsController.getSubjectsForProfessor(professor.Id)) {

                foreach (Student s in studentsSubjectsController.GetStudents(subject.Id)) 
                {
                    //MessageBox.Show(s.ID.ToString());
                    students.Add(s);
                }

            }

            RemoveDuplicates();

        }

        public void RemoveDuplicates()
        {
            var uniqueItems = new ObservableCollection<Student>();
            foreach (var item in students)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            students = uniqueItems;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }

        public void cancel_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
