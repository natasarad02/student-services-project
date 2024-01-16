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
    public partial class SubjectList2 : Window, IObserver
    {

        public ObservableCollection<SubjectDTO> Subjects { get; set; } //subjects without professor

        public SubjectDTO SelectedSubject { get; set; }

        public int professorID { get; set; }

        public string professorName { get; set; }


        private SubjectsController subjectController { get; set; }
        public SubjectList2(int prof_if, string prof_name, SubjectsController subjectController)
        {
            InitializeComponent();
            Subjects = new ObservableCollection<SubjectDTO>();
            this.subjectController = subjectController;
            subjectController.Subscribe(this);
            professorID = prof_if;
            professorName = prof_name;

            DataContext = this;


            Update();


        }
        public void Update()
        {
            Subjects.Clear();

            foreach (Subject subject in subjectController.getSubjectsWithoutProfessor())
            {
                Subjects.Add(new SubjectDTO(subject));
            }
        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {
            SelectedSubject.ProfessorId = professorID;
            SelectedSubject.ProfessorName = professorName;
            subjectController.Update(SelectedSubject.ToSubject());
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

    }
}
