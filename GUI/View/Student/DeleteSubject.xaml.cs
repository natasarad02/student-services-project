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

namespace GUI.View
{
    public partial class DeleteSubjectFromStudent : Window, INotifyPropertyChanged
    {
        public SubjectDTO SelectedSubject { get; set; }
        public StudentDTO Student { get; set; }
        private StudentsSubjectsController studentsSubjectController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SubjectDTO> Subjects{ get; set; }
        public DeleteSubjectFromStudent(StudentsSubjectsController studentsSubjectController, ObservableCollection<SubjectDTO> Subjects, StudentDTO Student, SubjectDTO SelectedSubject)
        {
            InitializeComponent();
            DataContext = this;
            this.SelectedSubject = SelectedSubject;
            this.studentsSubjectController = studentsSubjectController;
            this.Subjects = Subjects;
            this.Student = Student;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            studentsSubjectController.Delete(Student.Id, SelectedSubject.Id);
            Subjects.Remove(SelectedSubject);
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
