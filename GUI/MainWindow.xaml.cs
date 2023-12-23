using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using GUI.View;
using System.Collections.ObjectModel;
using StudentskaSluzba.Model;
using CLI.Observer;
using GUI.DTO;
using StudentskaSluzba.DAO;
using System.Windows;
using CLI.Controller;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ObservableCollection<DepartmentDTO> Departments { get; set; }

        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public SubjectDTO  SelectedSubject { get; set; }
        private SubjectDAO subjectsDAO { get; set; }

        private SubjectsController subjectController { get; set; }


        public  StudentDTO SelectedStudent { get; set; }
        private StudentsController studentController { get; set; }

        private DepartmentsController departmentController { get; set; }
        public DepartmentDTO SelectedDepartment { get; set; }
        
        public ProfessorDTO SelectedProfessor { get; set; }

        private ProfessorDAO professorsDAO { get; set; }


        private ProfessorsController professorController { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Students = new ObservableCollection<StudentDTO>();
            studentController = new StudentsController();
            studentController.Subscribe(this);

            Subjects = new ObservableCollection<SubjectDTO>();
            subjectController = new SubjectsController();
            subjectController.Subscribe(this);

            
           
            Professors = new ObservableCollection<ProfessorDTO>();
            professorController = new ProfessorsController();
            professorController.Subscribe(this);


                     
            Departments = new ObservableCollection<DepartmentDTO>();
            departmentController = new DepartmentsController();
            departmentController.Subscribe(this);
            DataContext = this;


            Update();

            // Get the screen dimensions
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Calculate 3/4 of the screen size
            double targetWidth = screenWidth * 0.75;
            double targetHeight = screenHeight * 0.75;

            // Set the window size
            Width = targetWidth;
            Height = targetHeight;

            Left = (screenWidth - targetWidth) / 2;
            Top = (screenHeight - targetHeight) / 2;

        }
        private void Add_Click(object sender, RoutedEventArgs e, SelectionChangedEventArgs sel_e)
        {
            string tabHeader = SelectionChanged(sender, sel_e);
            switch (tabHeader)
                   {
                       case "Students":
                           AddStudent addStudent = new AddStudent(studentController);
                           addStudent.Show();
                           break;
                       case "Subjects":
                           AddSubject addSubject = new AddSubject(subjectController);
                           addSubject.Show();
                           break;
                       case "Professors":
                   
                            AddProfessor addProfessor = new AddProfessor(professorController);
                            addProfessor.Show();

                            break;
                       case "Departments":
                           AddDepartment addDepartment = new AddDepartment(departmentController);
                           addDepartment.Show();
                           break;
                           
                   }

        }

        public void Update()
        {
            Students.Clear();
            foreach (Student student in studentController.GetAllStudents())
            {
                Students.Add(new StudentDTO(student));
            }
            Subjects.Clear();
            foreach(Subject subject in subjectController.GetAllSubjects())
            {
                Subjects.Add(new SubjectDTO(subject));
            }
        
            Professors.Clear();
            foreach (Professor professor in professorController.GetAllProfessors())
            {
                Professors.Add(new ProfessorDTO(professor));
            }

          

            Departments.Clear();
            foreach(Department department in departmentController.GetAllDepartments())
            {
                Departments.Add(new DepartmentDTO(department));
            }

        }

        private string SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                TabItem selectedTab = tabControl.SelectedItem as TabItem;

                if (selectedTab != null)
                {
                    string tabHeader = selectedTab.Header.ToString();


                    return tabHeader;
                    
                }
            }

            return null;

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                TabItem selectedTab = tabControl.SelectedItem as TabItem;

                if (selectedTab != null)
                {
                    string tabHeader = selectedTab.Header.ToString();

                    // Update the TextBlock text to display the current tab
                    currentTabTextBlock.Text = "Current Tab: " + tabHeader;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e, SelectionChangedEventArgs sel_e)
        {
           string tabHeader = "Students";
            switch (tabHeader)
            {
                case "Students":
                    // Access and manipulate Student entities
                    // Example: viewModel.DeleteStudent();
                    break;
                case "Subjects":
                    if (SelectedSubject == null)
                        MessageBox.Show("Please choose a subject to delete");
                    else
                        subjectController.Delete(SelectedSubject.Ids);
                    
                    break;
                case "Professors":

                    if (SelectedProfessor == null)
                        MessageBox.Show("Please choose a professor to delete");
                    else
                        professorController.Delete(SelectedProfessor.Num);

                    break;
                case "Departments":
                    // Access and manipulate Department entities
                    // Example: viewModel.UpdateDepartment();
                    break;
                    // Add more cases for additional tabs if needed
            }
        }
        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
