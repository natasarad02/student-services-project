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
using System.Windows.Controls;
namespace GUI.View
{
    public partial class UpdateStudent : Window, INotifyPropertyChanged, IObserver
    {
        public StudentDTO Student { get; set; }
        public SubjectDTO SelectedSubject { get; set; }

        public ExamGradeDTO SelectedGrade { get; set; }

        private StudentsController studentController;
        private SubjectsController subjectsController;
        private StudentsSubjectsController studentSubjectsController;
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public List<Subject> previousList { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool isUpdate = false;
        private ExamGradesController examGradesController { get; set; }

        public ObservableCollection<ExamGradeDTO> Grades { get; set; }

        public UpdateStudent(StudentsController studentController, StudentsSubjectsController studentSubjectsController)
        {
            InitializeComponent();
            
            DataContext = this;
            Student = new StudentDTO();

           
            subjectsController = new SubjectsController();
           
            Grades = new ObservableCollection<ExamGradeDTO>();
            examGradesController = new ExamGradesController();
            examGradesController.Subscribe(this);

            this.studentController = studentController;
            this.studentSubjectsController = studentSubjectsController;
            
            subjectsController = new SubjectsController();


            Subjects = new ObservableCollection<SubjectDTO>();

            studentSubjectsController.Subscribe(this);
            subjectsController.Subscribe(this);
         

            TabUpdate.SelectionChanged += TabUpdate_SelectionChanged;
            
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
            SubjectList subjectList = new SubjectList(Student, studentController, studentSubjectsController);
            subjectList.attendingSubjects = Subjects;
                        
            subjectList.Show();
        }

        public void Update()
        {
            Subjects.Clear();
            foreach (Subject subject in studentSubjectsController.GetAllSubjectsByStudent(Student.toStudent()))
            {
                Subjects.Add(new SubjectDTO(subject));
            }

            Grades.Clear();
            foreach (ExamGrade examGrade in examGradesController.getGradesForStudent(Student.Id)) 
            {
                Subject subject = subjectsController.findSubjectById(examGrade.subjectID);
                Grades.Add(new ExamGradeDTO(examGrade, subject.Name, subject.Espb, subject.Ids));
            }

        }

        private void TabUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabUpdate.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Subjects")
            {
                int tabIndex = TabUpdate.SelectedIndex;

                if (tabIndex == 1 && isUpdate == false)
                {
                        Update();
                        isUpdate = true;
                }

                if (tabIndex == 2 && isUpdate == false)
                { 
                        Update();
                        isUpdate = true;
                }

            }
        }


        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Delete_Subject_Click(object sender, RoutedEventArgs e)
        {
            studentSubjectsController.Delete(Student.Id, SelectedSubject.Id);
            Subjects.Remove(SelectedSubject);
           
        }

        public void Delete_Grade_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null) {
                MessageBox.Show("Please choose a grade to delete");
            }
            else
            {
                DeleteGrade deleteGrade = new DeleteGrade(examGradesController);
                deleteGrade.exam = SelectedGrade;
                deleteGrade.Show();
            }

        }

    }

}
