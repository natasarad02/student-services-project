using GUI.DTO;
using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CLI.Controller;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;
using CLI.Observer;

namespace GUI.View
{
    public partial class UpdateStudent : Window, INotifyPropertyChanged, IObserver
    {
        public StudentDTO Student { get; set; }

        private StudentsController studentController { get; set; }
        private SubjectsController subjectsController { get; set; }
        private StudentsSubjectsController studentSubjectsController { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SubjectDTO> attendingSubjects { get; set; }
        public UpdateStudent(StudentsController studentController, StudentsSubjectsController studentSubjectsController)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            attendingSubjects = new ObservableCollection<SubjectDTO>();
            subjectsController = new SubjectsController();
            subjectsController.Subscribe(this);

            this.studentController = studentController;
            this.studentSubjectsController = studentSubjectsController;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            studentController.Update(Student.toStudent());
  
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Subject(object sender, RoutedEventArgs e)
        {
            SubjectList subjectList = new SubjectList(Student, studentController, studentSubjectsController, attendingSubjects);
            subjectList.attendingSubjects = attendingSubjects;
            subjectList.Show();
        }

        public void Update()
        {

            attendingSubjects.Clear();
            foreach (Subject subject in subjectsController.GetAllSubjects())
            {
                attendingSubjects.Add(new SubjectDTO(subject));
            }



        }

    }

}
