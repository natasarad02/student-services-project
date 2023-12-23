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
        public SubjectDTO  SelectedSubject { get; set; }

        private SubjectDAO subjectsDAO { get; set; }

        public  StudentDTO SelectedStudent { get; set; }
        private StudentDAO studentDAO { get; set; }

        private DepartmentDAO departmentDAO { get; set; }
        public DepartmentDTO SelectedDepartment { get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }

        private ProfessorDAO professorsDAO { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Subjects = new ObservableCollection<SubjectDTO>();
            subjectsDAO = new SubjectDAO();
            subjectsDAO.SubjectSubject.Subscribe(this);

            
            DataContext = this;
            Professors = new ObservableCollection<ProfessorDTO>();
            professorsDAO = new ProfessorDAO();
            professorsDAO.ProfessorSubject.Subscribe(this);

            DataContext = this;
            Students = new ObservableCollection<StudentDTO>();
            studentDAO = new StudentDAO();
            studentDAO.StudentSubject.Subscribe(this);

            DataContext = this;
            Departments = new ObservableCollection<DepartmentDTO>();
            departmentDAO = new DepartmentDAO();
            departmentDAO.DepartmentSubject.Subscribe(this);

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
                           AddStudent addStudent = new AddStudent(studentDAO);
                           addStudent.Show();
                           break;
                       case "Subjects":
                           AddSubject addSubject = new AddSubject(subjectsDAO);
                           addSubject.Show();
                           break;
                       case "Professors":
                   
                            AddProfessor addProfessor = new AddProfessor(professorsDAO);
                            addProfessor.Show();

                            break;
                       case "Departments":
                           AddDepartment addDepartment = new AddDepartment(departmentDAO);
                           addDepartment.Show();
                           break;
                           
                   }

        }

        public void Update()
        {
            Subjects.Clear();
            foreach(Subject subject in subjectsDAO.GetAllSubjects())
            {
                Subjects.Add(new SubjectDTO(subject));
            }
        
            Professors.Clear();
            foreach (Professor professor in professorsDAO.GetAllProfessors())
            {
                Professors.Add(new ProfessorDTO(professor));
            }

            Students.Clear();
            foreach (Student student in studentDAO.GetAllStudents()) {
                Students.Add(new StudentDTO(student));  
            }

            Departments.Clear();
            foreach(Department department in departmentDAO.GetAllDepartments())
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
            string tabHeader = SelectionChanged(sender, sel_e);
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
                        subjectsDAO.RemoveSubject(SelectedSubject.Ids);
                    
                    break;
                case "Professors":

                    if (SelectedProfessor == null)
                        MessageBox.Show("Please choose a professor to delete");
                    else
                        subjectsDAO.RemoveSubject(SelectedProfessor.Num);

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
