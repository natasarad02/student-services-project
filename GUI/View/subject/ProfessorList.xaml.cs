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
    public partial class ProfessorList : Window, IObserver
    {

        private SubjectsController subjectController { get; set; }

        
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public ObservableCollection<ProfessorDTO> professorsSubjects { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }

        public SubjectDTO Subject { get; set; }
        private ProfessorsController professorController { get; set; }
        public ProfessorList(SubjectDTO Subject, SubjectsController subjectController, ProfessorsController professorController)
        {
            InitializeComponent();
            Professors = new ObservableCollection<ProfessorDTO>();
            this.professorController = professorController;
            professorController.Subscribe(this);
            this.subjectController = subjectController;
           
            professorsSubjects = new ObservableCollection<ProfessorDTO>();
            this.Subject = Subject;
            DataContext = this;


            Update();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;


            double targetWidth = screenWidth * 0.75;
            double targetHeight = screenHeight * 0.75;

            Width = targetWidth;
            Height = targetHeight;

            Left = (screenWidth - targetWidth) / 2;
            Top = (screenHeight - targetHeight) / 2;

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
            
           
        }



    }
}
