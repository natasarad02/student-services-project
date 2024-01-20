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
using GUI.Localization;
using System.Windows.Input;

namespace GUI.View
{
    public partial class ProfessorList : Window, IObserver
    {

        private SubjectsController subjectController { get; set; }

        
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public ObservableCollection<ProfessorDTO> professorsSubjects { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }
        public SubjectWindowInterface parentWindow { get; set; }
        public SubjectDTO Subject { get; set; }
        private ProfessorsController professorController { get; set; }

        
        public ProfessorList(SubjectDTO Subject, SubjectsController subjectController, ProfessorsController professorController, SubjectWindowInterface parentWindow)
        {
            InitializeComponent();
            Professors = new ObservableCollection<ProfessorDTO>();
            this.professorController = professorController;
            professorController.Subscribe(this);
            this.subjectController = subjectController;
            this.parentWindow = parentWindow;
            professorsSubjects = new ObservableCollection<ProfessorDTO>();
            this.Subject = Subject;
            DataContext = this;


            Update();

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
        public void Update()
        {

            Professors.Clear();
            

            List<Professor> tmpProfessorList = new List<Professor>();//subjectController.GetAllSubjects();
           

            foreach(Professor professor in professorController.GetAllProfessors())
                Professors.Add(new ProfessorDTO(professor));


        }

        private void Add_Professor_Click(object sender, RoutedEventArgs e)
        {

            if(SelectedProfessor == null)
            {
                MessageBox.Show("Please choose a professor to add");
            }
            else
            {
                Subject.ProfessorId = SelectedProfessor.Id;
                Subject.ProfessorName = SelectedProfessor.Name + " " + SelectedProfessor.Surname;

                Close();
            }
            parentWindow.IsEnabled = true;


        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }



    }
}
