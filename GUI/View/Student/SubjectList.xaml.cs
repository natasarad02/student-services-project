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

namespace GUI.View
{
    public partial class SubjectList : Window, IObserver
    {
       
        private StudentsController studentsController { get; set; }
        private StudentsSubjectsController studentsSubjectsController { get; set; }

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public SubjectDTO SelectedSubject { get; set; }

        public StudentDTO Student { get; set; }
        private SubjectsController subjectController { get; set; }
        public SubjectList(StudentDTO Student, StudentsController studentController, StudentsSubjectsController studentsSubjectsController)
        {
            InitializeComponent();
            Subjects = new ObservableCollection<SubjectDTO>();
            subjectController = new SubjectsController();
            subjectController.Subscribe(this);
            studentsController = studentController;
            

            this.Student = Student;
            this.studentsSubjectsController = studentsSubjectsController;
            DataContext = this;


            Update();

           
        }
        public void Update()
        {

            Subjects.Clear();
            foreach (Subject subject in subjectController.GetAllSubjects())
            {
                Subjects.Add(new SubjectDTO(subject));
            }



        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {
            
            studentsSubjectsController.Add(Student.Id, SelectedSubject.Id);
            Close();
        }
    }
}
