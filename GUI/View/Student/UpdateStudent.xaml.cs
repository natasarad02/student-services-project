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

        private StudentsController studentController;
        private SubjectsController subjectsController;
        private StudentsSubjectsController studentSubjectsController;
        public ObservableCollection<SubjectDTO> attendingSubjects { get; set; }
        public List<Subject> previousList { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private ExamGradesController examGradesController { get; set; }

        public ObservableCollection<ExamGradeDTO> Grades { get; set; }

        public UpdateStudent(StudentsController studentController, StudentsSubjectsController studentSubjectsController)
        {
            InitializeComponent();
            
            DataContext = this;
            Student = new StudentDTO();

           
            subjectsController = new SubjectsController();
           // subjectsController.Subscribe(this);


            // subjectsController = new SubjectsController();
            //subjectsController.Subscribe(this);


            Grades = new ObservableCollection<ExamGradeDTO>();
            examGradesController = new ExamGradesController();
            examGradesController.Subscribe(this);

            this.studentController = studentController;
            this.studentSubjectsController = studentSubjectsController;
            
           // subjectsController = new SubjectsController();


            attendingSubjects = new ObservableCollection<SubjectDTO>();

            // studentSubjectsController.Subscribe(this);
            //subjectsController.Subscribe(this);
            //


            TabUpdate.SelectionChanged += TabUpdate_SelectionChanged;
            /* if (TabUpdate.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Subjects")
             {
                 Update();
             }*/

            // previousList = new List<Subject>();
            // previousList = studentSubjectsController.GetAllSubjectsById(Student.Id);


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
            subjectList.attendingSubjects = attendingSubjects;
            //;
            
            subjectList.Show();
        }

        public void Update()
        {
            attendingSubjects.Clear();


            foreach (Subject subject in studentSubjectsController.GetAllSubjectsByStudent(Student.toStudent()))
            {
                attendingSubjects.Add(new SubjectDTO(subject));
            }

            Grades.Clear();
                foreach (ExamGrade examGrade in examGradesController.getGradesForStudent(Student.Id)) {
                    Grades.Add(new ExamGradeDTO(examGrade));
                    //kako dodati ime predmeta?
                }
            
        }




        
        private void TabUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabUpdate.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Subjects")
            {
                // Get the index of the selected tab
                int tabIndex = TabUpdate.SelectedIndex;

                // Check if the selected tab is the "Subjects" tab
                if (tabIndex == 1)
                {
                    // Trigger the Update() method
                    Update();
                }
            }
        }
    }

}
