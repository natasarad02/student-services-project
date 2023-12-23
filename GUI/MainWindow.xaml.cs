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
        public SubjectDTO  SelectedSubject { get; set; }

        private SubjectDAO subjectsDAO { get; set; }

        private StudentDAO studentDAO { get; set; }
        private DepartmentDAO departmentDAO { get; set; }

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

            Professors = new ObservableCollection<ProfessorDTO>();
            professorsDAO = new ProfessorDAO();
            professorsDAO.ProfessorSubject.Subscribe(this);

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
                           
                           break;
                           // Add more cases for additional tabs if needed
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
                    // Perform actions based on the selected tab
                    /*switch (tabHeader)
                    {
                        case "Students":
                            // Access and manipulate Student entities
                            // Example: viewModel.DeleteStudent();
                            break;
                        case "Subjects":
                            // Access and manipulate Subject entities
                            // Example: viewModel.AddSubject();
                            break;
                        case "Professors":
                            // Access and manipulate Professor entities
                            // Example: viewModel.EditProfessor();
                            break;
                        case "Departments":
                            // Access and manipulate Department entities
                            // Example: viewModel.UpdateDepartment();
                            break;
                            // Add more cases for additional tabs if needed
                    }*/
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

       // private void Delete_Cli
        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
