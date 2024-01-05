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
    public partial class StudentProfessorList : Window, IObserver
    {

        private StudentsSubjectsController studentsSubjectsController { get; set; }

        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        
        public SubjectDTO SelectedStudent { get; set; }

        public StudentDTO Student { get; set; }
        private SubjectsController subjectController { get; set; }
        private ProfessorsController professorsController { get; set; }

       
        public StudentProfessorList(StudentDTO Student,  StudentsSubjectsController studentsSubjectsController,ProfessorsController professorsController, SubjectsController subjectController)
        {
            InitializeComponent();

            Professors = new ObservableCollection<ProfessorDTO>();
            this.subjectController = subjectController;
            this.professorsController = professorsController;
            professorsController.Subscribe(this);
            
            this.Student = Student;
            this.studentsSubjectsController = studentsSubjectsController;
             DataContext = this;


            Update();


        }
        public void Update()
        {

            Professors.Clear();


            foreach (Subject subject in studentsSubjectsController.GetAllSubjectsByStudent(Student.toStudent(), subjectController))
            {

                bool duplicate = false;
                Professor tmpProfessor = subjectController.getProfessorForSubject(subject, professorsController.GetAllProfessors());
                if (tmpProfessor != null)
                {
                    foreach (ProfessorDTO prof in Professors)
                    {
                        if (prof.Id == tmpProfessor.Id)
                        {
                            duplicate = true;
                            break;
                        }
                    }

                    if (duplicate == false)
                        Professors.Add(new ProfessorDTO(tmpProfessor));
                }

            }



        }

       



    }
}
