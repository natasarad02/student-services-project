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
using System.Windows.Threading;
using System.ComponentModel;
using GUI.View.Help;

namespace GUI
{
      public partial class MainWindow : Window, IObserver
    {

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ObservableCollection<DepartmentDTO> Departments { get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

       // public ObservableCollection<SubjectDTO> attendingSubjects { get; set; }
        public SubjectDTO  SelectedSubject { get; set; }

        private SubjectsController subjectController { get; set; }


        public  StudentDTO SelectedStudent { get; set; }
        private StudentsController studentController { get; set; }

        private DepartmentsController departmentController { get; set; }
        public DepartmentDTO SelectedDepartment { get; set; }
        
        public ProfessorDTO SelectedProfessor { get; set; }

        private ProfessorsController professorController { get; set; }

        //---ostali kontroleri---
        private ExamGradesController examGradesController { get; set; }
        private StudentsSubjectsController studentsSubjectsController { get; set; }
        //-----------------------

        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
                      

            Students = new ObservableCollection<StudentDTO>();
            studentController = new StudentsController();
            studentController.Subscribe(this);

            Subjects = new ObservableCollection<SubjectDTO>();
            subjectController = new SubjectsController();
            subjectController.Subscribe(this);

            studentsSubjectsController = new StudentsSubjectsController();
            studentsSubjectsController.Subscribe(this);
            //attendingSubjects = new ObservableCollection<SubjectDTO>();
           

            Professors = new ObservableCollection<ProfessorDTO>();
            professorController = new ProfessorsController();
            professorController.Subscribe(this);


                     
            Departments = new ObservableCollection<DepartmentDTO>();
            departmentController = new DepartmentsController();
            departmentController.Subscribe(this);

            StudentDataGrid.ItemsSource = Students;
            ProfessorsDataGrid.ItemsSource = Professors;
            SubjectsDataGrid.ItemsSource = Subjects;
            DepartmentDataGrid.ItemsSource = Departments;
            
            DataContext = this;


            Update();

            
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            
            double targetWidth = screenWidth * 0.75;
            double targetHeight = screenHeight * 0.75;

            Width = targetWidth;
            Height = targetHeight;

            Left = (screenWidth - targetWidth) / 2;
            Top = (screenHeight - targetHeight) / 2;

            // vreme i datum
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateDateTime;
            timer.Start();

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearchBox.Text; 

            int tabIndex = Tab.SelectedIndex;
            switch (tabIndex)
            {
                case 0: //student
                    studentSearch(query);
                    break;
                case 1: // subject
                    subjectSearch(query);
                    break;
                case 2: // profesor
                    professorSearch(query);
                    break;
                case 3: //departman, za ovo ne treba search
                    
                    break;

            }

        }

        private void professorSearch(string query)
        {

            string[] words = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }

            string idnum = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (words.Length == 1)
            {
                lastName = words[0];
            }
            else if (words.Length == 2)
            {
                lastName = words[0];
                firstName = words[1];
            }
            else if (words.Length >= 3)
            {

                idnum = words[0];
                firstName = words[1];
                lastName = string.Join(" ", words.Skip(2));
            }

            var searchResults = Professors.Where(professor =>
                                (string.IsNullOrEmpty(idnum) || professor.Num.ToString().ToUpper().Contains(idnum.ToUpper())) &&
                                (string.IsNullOrEmpty(firstName) || professor.Name.ToUpper().Contains(firstName.ToUpper())) &&
                                (string.IsNullOrEmpty(lastName) || professor.Surname.ToUpper().Contains(lastName.ToUpper()))
                            ).ToList();

           ProfessorsDataGrid.ItemsSource = searchResults;
        }
        private void studentSearch(string query) {
            
            string[] words = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }

            string index = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (words.Length == 1)
            {
                lastName = words[0]; 
            }
            else if (words.Length == 2)
            {
                lastName = words[0]; 
                firstName = words[1]; 
            }
            else if (words.Length >= 3)
            {
                
                index = words[0]; 
                firstName = words[1]; 
                lastName = string.Join(" ", words.Skip(2)); 
            }

            var searchResults = Students.Where(student =>
                                (string.IsNullOrEmpty(index) || student.getIndeks().ToUpper().Contains(index.ToUpper())) &&
                                (string.IsNullOrEmpty(firstName) || student.First_Name.ToUpper().Contains(firstName.ToUpper())) &&
                                (string.IsNullOrEmpty(lastName) || student.Last_Name.ToUpper().Contains(lastName.ToUpper()))
                            ).ToList();

            StudentDataGrid.ItemsSource = searchResults;
        }

        private void subjectSearch(string query)
        {

            string[] words = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }

            string ids = string.Empty;
            string subjectName = string.Empty;
            
            if (words.Length == 1)
            {
                subjectName = words[0];
            }
            else if (words.Length >= 2)
            {
                subjectName = words[0];
                ids = words[1];
            }
           

            var searchResults = Subjects.Where(subject =>
                                (string.IsNullOrEmpty(ids) || subject.Ids.ToString().ToUpper().Contains(ids.ToUpper())) &&
                                (string.IsNullOrEmpty(subjectName) || subject.Name.ToUpper().Contains(subjectName.ToUpper()))).ToList();

            SubjectsDataGrid.ItemsSource = searchResults;
        }

        private void UpdateDateTime(object sender, EventArgs e)
        {
            dateTimeTextBlock.Text = DateTime.Now.ToString("dddd, yyyy-MM-dd HH:mm:ss");
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int tabIndex = Tab.SelectedIndex;
            switch (tabIndex)
                   {
                       case 0:
                           AddStudent addStudent = new AddStudent(studentController);
                           addStudent.Show();
                           break;
                       case 1:
                           AddSubject addSubject = new AddSubject(subjectController);
                           addSubject.Show();
                           break;
                       case 2:
                   
                            AddProfessor addProfessor = new AddProfessor(professorController);
                            addProfessor.Show();

                            break;
                       case 3:
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


           /* foreach (Student student in studentController.GetAllStudents())
            {

                attendingSubjects.Clear();
                //int broj = studentsSubjectsController.GetAllSubjectsById(student.ID).Count;
               // MessageBox.Show(broj.ToString());
                foreach (Subject subject in studentsSubjectsController.GetAllSubjectsById(student.ID))
                {
                    attendingSubjects.Add(new SubjectDTO(subject));
                }


            }*/






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

                     currentTabTextBlock.Text = "Current Tab: " + tabHeader;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
           int tabIndex = Tab.SelectedIndex;
            switch (tabIndex)
            {
                case 0:
                    if (SelectedStudent == null)
                        MessageBox.Show("Please choose a student to delete");
                    else
                    {
                        DeleteStudent deleteStudent = new DeleteStudent(studentController);
                        deleteStudent.Student = SelectedStudent;
                        deleteStudent.Show();
                    }
                    break;
                case 1:
                    if (SelectedSubject == null)
                        MessageBox.Show("Please choose a subject to delete");
                    else
                    {
                        DeleteSubject deleteSubject = new DeleteSubject(subjectController);
                        deleteSubject.Subject = SelectedSubject;
                        deleteSubject.Show();
                    }
                    break;
                case 2:

                    if (SelectedProfessor == null)
                        MessageBox.Show("Please choose a professor to delete");
                    else
                    {
                        DeleteProfessor deleteProfessor = new DeleteProfessor(professorController);
                        deleteProfessor.Professor = SelectedProfessor;
                        deleteProfessor.Show();
                    }
                    break;
                case 3:
                    if (SelectedDepartment == null)
                        MessageBox.Show("Please choose a department to delete");
                    else
                    {
                        DeleteDepartment deleteDepartment = new DeleteDepartment(departmentController);
                        deleteDepartment.department = SelectedDepartment;
                        deleteDepartment.Show();
                    }
                    break;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            int tabIndex = Tab.SelectedIndex;
            switch (tabIndex)
            {
                case 0:
                    if (SelectedStudent == null)
                        MessageBox.Show("Please choose a student to update!");
                    else {
                        UpdateStudent updateStudent = new UpdateStudent(studentController, studentsSubjectsController);
                        updateStudent.Student = SelectedStudent;
                       // updateStudent.previousList = studentsSubjectsController.GetAllSubjectsById(SelectedStudent.Id);
                        // attendingSubjects = new ObservableCollection<SubjectDTO>();
                       // updateStudent.attendingSubjects = attendingSubjects;
                       
                        updateStudent.Show();
                       // updateStudent.Update();

                    }
                    break;
                case 1:
                    if (SelectedSubject == null)
                        MessageBox.Show("Please choose a subject to update");
                    else
                    {
                        UpdateSubject updateSubject = new UpdateSubject(SelectedSubject, subjectController);
                        //updateSubject.Subject = SelectedSubject;
                        updateSubject.Show();
                     }

                    break;
                case 2:

                    if (SelectedProfessor == null)
                        MessageBox.Show("Please choose a professor to update");
                    else
                    {
                        UpdateProfessor updateProfessor = new UpdateProfessor(professorController);
                        updateProfessor.Professor = SelectedProfessor;
                        //updateProfessor.Professor.Address = SelectedProfessor.Address;
                        updateProfessor.Show();
                    }
                    break;
                case 3:
                    if (SelectedDepartment == null)
                        MessageBox.Show("Please choose a department to update");
                    else {
                        UpdateDepartment updateDepartment = new UpdateDepartment(departmentController);
                        updateDepartment.department = SelectedDepartment;
                        updateDepartment.Show();
                    }                    
                    break;
                 }
        }


        public void Help_Click(object sender, RoutedEventArgs e) {
            StudentServiceInfo studentServiceInfo = new StudentServiceInfo();
            studentServiceInfo.Show();
        }

        private void Open_Student_Click(object sender, EventArgs e)
        {
            Tab.SelectedIndex = 0;
        }
        private void Open_Subject_Click(object sender, EventArgs e)
        {
            Tab.SelectedIndex = 1;
        }
        private void Open_Professor_Click(object sender, EventArgs e)
        {
            Tab.SelectedIndex = 2;
        }
        private void Open_Department_Click(object sender, EventArgs e)
        {
            Tab.SelectedIndex = 3;
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
        }

        private void Click_Save(object sender, RoutedEventArgs e) {
            studentController.Save();
            subjectController.Save();
            professorController.Save();
            departmentController.Save();
            //studentsSubjectsController.Save(); jos nema pa puca
            // examGradesController.Save();
            MessageBox.Show("Save successfull!");
        }

    }

}
