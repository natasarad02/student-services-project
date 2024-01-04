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
        public ProfessorList(SubjectDTO Subject, SubjectsController subjectController)
        {
            InitializeComponent();
            Professors = new ObservableCollection<ProfessorDTO>();
            professorController = new ProfessorsController();
            professorController.Subscribe(this);
            this.subjectController = subjectController;

            professorsSubjects = new ObservableCollection<ProfessorDTO>();
            this.Subject = Subject;
            DataContext = this;


            Update();


        }
        public void Update()
        {

            Professors.Clear();
            // bool subjectIsFound = false;

            List<Professor> tmpProfessorList = new List<Professor>();//subjectController.GetAllSubjects();
            /*foreach (Subject subject in subjectController.GetAllSubjects())
            {
                if (subject.year >= Student.Current_Year)
                {
                    tmpSubjectList.Add(subject);
                }
            }
            foreach (Subject subject in subjectController.GetAllSubjects())
            {


                // MessageBox.Show(subject.Id.ToString());
                foreach (Subject attendingSubject in studentsSubjectsController.GetAllSubjectsByStudent(Student.toStudent()))
                {


                    // MessageBox.Show(attendingSubject.Id.ToString());
                    if (subject.Id == attendingSubject.Id)
                    {

                        //subjectIsFound = true;
                        // MessageBox.Show("Predmet je pronadjen");
                        tmpSubjectList.Remove(subject);
                        break;

                    }



                }


            }
            foreach (Subject subject in tmpSubjectList)
                Subjects.Add(new SubjectDTO(subject));
            */

            foreach(Professor professor in professorController.GetAllProfessors())
                Professors.Add(new ProfessorDTO(professor));


        }

        private void Add_Professor_Click(object sender, RoutedEventArgs e)
        {

            Subject.ProfessorId = SelectedProfessor.Id;
            Subject.ProfessorName = SelectedProfessor.Name + " " + SelectedProfessor.Surname;
            Close();
           
        }



    }
}
