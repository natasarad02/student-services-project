using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GUI.View;
using System.Collections.ObjectModel;
using StudentskaSluzba.Model;
using CLI.Observer;
using GUI.DTO;
using CLI.Controller;
using System.Windows.Threading;
using GUI.View.Help;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using StudentskaSluzba.DAO;
using GUI.Localization;

namespace GUI
{
    public partial class MainWindow : Window, IObserver
    {

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public ObservableCollection<StudentDTO> Students { get; set; }
        public ObservableCollection<DepartmentDTO> Departments { get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public SubjectDTO SelectedSubject { get; set; }

        private SubjectsController subjectController { get; set; }


        public StudentDTO SelectedStudent { get; set; }
        private StudentsController studentController { get; set; }

        private DepartmentsController departmentController { get; set; }
        public DepartmentDTO SelectedDepartment { get; set; }

        public ProfessorDTO SelectedProfessor { get; set; }

        private ProfessorsController professorController { get; set; }

        //---ostali kontroleri---
        private ExamGradesController examGradesController { get; set; }
        private StudentsSubjectsController studentsSubjectsController { get; set; }
        //-----------------------
        private List<Student> searchStudents { get; set; }  
        private List<Subject> searchSubjects { get; set; }

        private List<Professor> searchProfessors { get; set; }
        private List<Department> searchDepartments { get; set; }

        private DispatcherTimer timer;

        private int itemsPerPage = 16;
        private int currentPageStudent = 0;
        private int currentPageSubject = 0;
        private int currentPageDepartment = 0;
        private int currentPageProfessor = 0;

        private bool isUpdateStudent = false;
        private bool isUpdateProfessor = false;
        private bool isUpdateSubject = false;
        private bool isUpdateDepartment = false;

        private int studentCount = 0;
        private int subjectCount = 0;
        private int professorCount = 0;
        //private int departmentCount = 0;
        private ObservableCollection<SubjectDTO> selectedSubjects = new ObservableCollection<SubjectDTO>();

        public SubjectDTO SelectedSubject2 { get; set; }
        public ListSortDirection newDirection;
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

            searchStudents = new List<Student>();
            searchSubjects = new List<Subject>();
           // searchDepartments = new List<Department>();
            searchProfessors = new List<Professor>();
            examGradesController = new ExamGradesController();
            //studentCount = studentController.GetAllStudents().Count;
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

            Tab.SelectionChanged += TabControl_SelectionChanged;
           StudentDataGrid.Sorting += Student_Sorting;
            studentCount = studentController.GetAllStudents().Count;
            subjectCount = subjectController.GetAllSubjects().Count;
            professorCount = professorController.GetAllProfessors().Count;
            //newDirection = ListSortDirection.Ascending;
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");

        }

        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the existing collection and add the newly selected items in order
            selectedSubjects.Clear();
            foreach (SubjectDTO selectedItem in SubjectsDataGrid.SelectedItems)
            {
                selectedSubjects.Add(selectedItem);
            }

            // Ensure only two items are selected
            while (selectedSubjects.Count > 2)
            {
                selectedSubjects.RemoveAt(2);
            }

            // Update SelectedSubject1 and SelectedSubject2 based on the selection
            if (selectedSubjects.Count >= 1)
            {
                SelectedSubject = selectedSubjects[0];
            }

            if (selectedSubjects.Count >= 2)
            {
                SelectedSubject2 = selectedSubjects[1];
            }

            if (selectedSubjects.Count() == 2)
            {
                Request request = new Request(SelectedSubject, SelectedSubject2, this);
                request.Show();
            }

        }
        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearchBox.Text; 

            int tabIndex = Tab.SelectedIndex;
            switch (tabIndex)
            {
                case 0: //student
                    searchStudents = studentSearch(query);
                    break;
                case 1: // subject
                    searchSubjects = subjectSearch(query);
                    break;
                case 2: // profesor
                    searchProfessors= professorSearch(query);
                    break;
                case 3: //departman, za ovo ne treba search
                    
                    break;

            }

        }

        private List<Professor> professorSearch(string query)
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
            ObservableCollection<ProfessorDTO> professorsTotal = new ObservableCollection<ProfessorDTO>();
            foreach (Professor p in professorController.GetAllProfessors())
                professorsTotal.Add(new ProfessorDTO(p));

            var searchResults = professorsTotal.Where(professor =>
                                (string.IsNullOrEmpty(idnum) || professor.Num.ToString().ToUpper().Contains(idnum.ToUpper())) &&
                                (string.IsNullOrEmpty(firstName) || professor.Name.ToUpper().Contains(firstName.ToUpper())) &&
                                (string.IsNullOrEmpty(lastName) || professor.Surname.ToUpper().Contains(lastName.ToUpper()))
                            ).ToList();

            int totalItems = searchResults.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            currentPageStudent = 0;

            List<Professor> results = new List<Professor>();
            foreach (ProfessorDTO s in searchResults)
                results.Add(s.ToProfessor());
            currentPageProfessor = 0;
            //UpdateWithPaging(currentPageStudent, itemsPerPage);
            UpdateWithPagingProfessor(currentPageProfessor, itemsPerPage, results);
            professorCount = results.Count;
            //StudentDataGrid.ItemsSource = searchResults;
            return results;
        }
        private List<Student> studentSearch(string query) {
            
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

            ObservableCollection<StudentDTO> studentsTotal = new ObservableCollection<StudentDTO>();
            foreach(Student s in studentController.GetAllStudents()) 
                studentsTotal.Add(new StudentDTO(s));
            
           
            var searchResults = studentsTotal.Where(student =>
                                (string.IsNullOrEmpty(index) || student.getIndeks().ToUpper().Contains(index.ToUpper())) &&
                                (string.IsNullOrEmpty(firstName) || student.First_Name.ToUpper().Contains(firstName.ToUpper())) &&
                                (string.IsNullOrEmpty(lastName) || student.Last_Name.ToUpper().Contains(lastName.ToUpper()))
                            ).ToList();

            int totalItems = searchResults.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            currentPageStudent = 0;

            List<Student> results = new List<Student>();
            foreach (StudentDTO s in searchResults)
                results.Add(s.toStudent());
            currentPageStudent = 0;
            //UpdateWithPaging(currentPageStudent, itemsPerPage);
            UpdateWithPagingStudent(currentPageStudent, itemsPerPage, results);
            studentCount = results.Count;
            //StudentDataGrid.ItemsSource = searchResults;
            return results;


        }

        private List<Subject> subjectSearch(string query)
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
            ObservableCollection<SubjectDTO> subjectsTotal = new ObservableCollection<SubjectDTO>();
            foreach (Subject s in subjectController.GetAllSubjects())
                subjectsTotal.Add(new SubjectDTO(s));

            var searchResults = subjectsTotal.Where(subject =>
                                (string.IsNullOrEmpty(ids) || subject.Ids.ToString().ToUpper().Contains(ids.ToUpper())) &&
                    
                                (string.IsNullOrEmpty(subjectName) || subject.Name.ToUpper().Contains(subjectName.ToUpper()))).ToList();



            int totalItems = searchResults.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            currentPageStudent = 0;

            List<Subject> results = new List<Subject>();
            foreach (SubjectDTO s in searchResults)
                results.Add(s.ToSubject());
            currentPageStudent = 0;
            //UpdateWithPaging(currentPageStudent, itemsPerPage);
            UpdateWithPagingSubject(currentPageStudent, itemsPerPage, results);
            studentCount = results.Count;
            //StudentDataGrid.ItemsSource = searchResults;
            return results;


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
                          
                           AddStudent addStudent = new AddStudent(studentController, this, studentCount);
                           currentPageStudent = 0;
                            
;                           addStudent.Show();
                            

                            break;
                       case 1:
                           AddSubject addSubject = new AddSubject(subjectController, professorController, this);
                            currentPageSubject = 0;
                            addSubject.Show();
                           break;
                       case 2:
                   
                            AddProfessor addProfessor = new AddProfessor(professorController, this);
                            currentPageProfessor = 0;
                            addProfessor.Show();

                            break;
                       case 3:
                           AddDepartment addDepartment = new AddDepartment(departmentController, this);
                            currentPageDepartment = 0;
                            addDepartment.Show();
                           
                           break;
                           
                   }

        }

        public void Update()
        {
            
            UpdateWithPaging(0, 16);
            studentCount = studentController.GetAllStudents().Count;
            subjectCount = subjectController.GetAllSubjects().Count;
            professorCount = professorController.GetAllProfessors().Count;
          //  departmentCount = departmentController.GetAllDepartments().Count;

        }
        public void UpdateWithPaging(int page, int itemsPerPage)
        {
           
            Students.Clear();
            
            foreach (Student student in studentController.GetStudentsPage(page, itemsPerPage))
            {
                student.Average_Grade = studentController.average_grade(student.ID, examGradesController);
                
                Students.Add(new StudentDTO(student));
            }

            Subjects.Clear();
            foreach(Subject subject in subjectController.GetSubjectsPage(page, itemsPerPage))
            {
                Subjects.Add(new SubjectDTO(subject));
            }
        
            Professors.Clear();
            foreach (Professor professor in professorController.GetProfessorsPage(page, itemsPerPage))
            {
                Professors.Add(new ProfessorDTO(professor));
            }

            Departments.Clear();
            foreach(Department department in departmentController.GetDepartmentsPage(page, itemsPerPage))
            {
                Departments.Add(new DepartmentDTO(department));
            }





        }
        public void UpdateWithPagingStudent(int page, int itemsPerPage, List<Student> students)
        {

            Students.Clear();
            foreach (Student student in studentController.GetStudentsPageSearch(page, itemsPerPage, students))
            {
                Students.Add(new StudentDTO(student));
            }
            

        }
        public void UpdateWithPagingProfessor(int page, int itemsPerPage, List<Professor> professors)
        {
            Professors.Clear();
            foreach (Professor professor in professorController.GetProfessorsPageSearch(page, itemsPerPage, professors))
            {
                Professors.Add(new ProfessorDTO(professor));
            }
        }

        public void UpdateWithPagingSubject(int page, int itemsPerPage, List<Subject> subjects)
        {
            Subjects.Clear();
            foreach (Subject subject in subjectController.GetSubjectsPageSearch(page, itemsPerPage, subjects))
            {
                Subjects.Add(new SubjectDTO(subject));
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

                      currentTabTextBlock.Text = "Current Tab: " + tabHeader;

                    switch (tabHeader)
                    {
                        case "Students":
                            if(isUpdateStudent == false)
                            {
                                currentPageStudent = 0;
                                UpdateWithPaging(currentPageStudent, itemsPerPage);
                                isUpdateStudent = true;
                            }
                           
                            break;
                        case "Subjects":
                            if(isUpdateSubject ==  false)
                            {
                                currentPageSubject = 0;
                                UpdateWithPaging(currentPageSubject, itemsPerPage);
                                isUpdateSubject = true;

                            }
                            
                            break;
                        case "Professors":
                            if(isUpdateProfessor == false)
                            {
                                currentPageProfessor = 0;
                                UpdateWithPaging(currentPageProfessor, itemsPerPage);
                                isUpdateProfessor = true;
                            }
                           
                            break;
                        case "Departments":
                            if(isUpdateDepartment == false)
                            {
                                currentPageDepartment = 0;
                                UpdateWithPaging(currentPageDepartment, itemsPerPage);
                                isUpdateDepartment = true;
                            }
                           
                            break;
                        case "Studenti":
                            if (isUpdateStudent == false)
                            {
                                currentPageStudent = 0;
                                UpdateWithPaging(currentPageStudent, itemsPerPage);
                                isUpdateStudent = true;
                            }

                            break;
                        case "Predmeti":
                            if (isUpdateSubject == false)
                            {
                                currentPageSubject = 0;
                                UpdateWithPaging(currentPageSubject, itemsPerPage);
                                isUpdateSubject = true;

                            }

                            break;
                        case "Profesori":
                            if (isUpdateProfessor == false)
                            {
                                currentPageProfessor = 0;
                                UpdateWithPaging(currentPageProfessor, itemsPerPage);
                                isUpdateProfessor = true;
                            }

                            break;
                        case "Katedre":
                            if (isUpdateDepartment == false)
                            {
                                currentPageDepartment = 0;
                                UpdateWithPaging(currentPageDepartment, itemsPerPage);
                                isUpdateDepartment = true;
                            }

                            break;
                    }
                    

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
                        DeleteStudent deleteStudent = new DeleteStudent(studentController, this, examGradesController);
                        deleteStudent.Student = SelectedStudent;
                        currentPageStudent = 0;
                        deleteStudent.Show();
                    }
                    break;
                case 1:
                    if (SelectedSubject == null)
                        MessageBox.Show("Please choose a subject to delete");
                    else
                    {
                        DeleteSubject deleteSubject = new DeleteSubject(subjectController, this);
                        deleteSubject.Subject = SelectedSubject;
                        currentPageSubject = 0;
                        deleteSubject.Show();
                    }
                    break;
                case 2:

                    if (SelectedProfessor == null)
                        MessageBox.Show("Please choose a professor to delete");
                    else
                    {
                        DeleteProfessor deleteProfessor = new DeleteProfessor(professorController, this, subjectController, departmentController);
                        deleteProfessor.Professor = SelectedProfessor;
                        currentPageProfessor = 0;
                        deleteProfessor.Show();
                    }
                    break;
                case 3:
                    if (SelectedDepartment == null)
                        MessageBox.Show("Please choose a department to delete");
                    else
                    {
                        DeleteDepartment deleteDepartment = new DeleteDepartment(departmentController, this);
                        deleteDepartment.department = SelectedDepartment;
                        currentPageDepartment = 0;
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
                        UpdateStudent updateStudent = new UpdateStudent(SelectedStudent, studentController, studentsSubjectsController, subjectController, professorController, examGradesController, this);
                        //updateStudent.Student = SelectedStudent;
                        // updateStudent.previousList = studentsSubjectsController.GetAllSubjectsById(SelectedStudent.Id);
                        // attendingSubjects = new ObservableCollection<SubjectDTO>();
                        // updateStudent.attendingSubjects = attendingSubjects;
                        currentPageStudent = 0;
                        updateStudent.Show();
                       // updateStudent.Update();

                    }
                    break;
                case 1:
                    if (SelectedSubject == null)
                        MessageBox.Show("Please choose a subject to update");
                    else
                    {
                        UpdateSubject updateSubject = new UpdateSubject(SelectedSubject, subjectController, professorController, this);
                        //updateSubject.Subject = SelectedSubject;
                        currentPageSubject = 0;
                        updateSubject.Show();
                     }

                    break;
                case 2:

                    if (SelectedProfessor == null)
                        MessageBox.Show("Please choose a professor to update");
                    else
                    {
                        UpdateProfessor updateProfessor = new UpdateProfessor(professorController, SelectedProfessor, this);
                        currentPageProfessor = 0;
                        updateProfessor.Show();
                    }
                    break;
                case 3:
                    if (SelectedDepartment == null)
                        MessageBox.Show("Please choose a department to update");
                    else {
                        UpdateDepartment updateDepartment = new UpdateDepartment(departmentController, professorController, subjectController, this, SelectedDepartment);
                        updateDepartment.department = SelectedDepartment;
                        currentPageDepartment = 0;
                        updateDepartment.Show();
                    }                    
                    break;
                 }
        }


        public void English_Click(object sender, RoutedEventArgs e) 
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        public void Serbian_Click(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-RS");
        }

        public void Help_Click(object sender, RoutedEventArgs e) {
            StudentServiceInfo studentServiceInfo = new StudentServiceInfo(this);
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


        private void PreviousPageStudent_Click(object sender, RoutedEventArgs e)
        {
            int totalStudents = studentCount;
            if (currentPageStudent > 0)
            {
                if(studentCount < studentController.GetAllStudents().Count())
                {
                    currentPageStudent--;
                    UpdateWithPagingStudent(currentPageStudent, itemsPerPage, searchStudents);
                }
                else
                {
                    currentPageStudent--;
                    UpdateWithPaging(currentPageStudent, itemsPerPage);
                }
                
            }

        }
        private void PreviousPageDepartment_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageDepartment > 0)
            {
                currentPageDepartment--;
                UpdateWithPaging(currentPageDepartment, itemsPerPage);
            }
        }
        private void PreviousPageProfessor_Click(object sender, RoutedEventArgs e)
        {
            int totalProfessors = professorCount;
            if (currentPageProfessor > 0)
            {
                if (professorCount < professorController.GetAllProfessors().Count())
                {
                    currentPageProfessor--;
                    UpdateWithPagingProfessor(currentPageProfessor, itemsPerPage, searchProfessors);
                }
                else
                {
                    currentPageProfessor--;
                    UpdateWithPaging(currentPageProfessor, itemsPerPage);
                }
            }

        }
        private void PreviousPageSubject_Click(object sender, RoutedEventArgs e)
        {
            int totalSubjects = subjectCount;
            if (currentPageSubject > 0)
            {
                if (subjectCount < subjectController.GetAllSubjects().Count())
                {
                    currentPageSubject--;
                    UpdateWithPagingSubject(currentPageSubject, itemsPerPage, searchSubjects);
                }
                else
                {
                    currentPageSubject--;
                    UpdateWithPaging(currentPageSubject, itemsPerPage);
                }
            }


        }



        private void NextPageStudent_Click(object sender, RoutedEventArgs e)
        {
            int totalStudents = studentCount;
          
            int totalStudentPages = (int)Math.Ceiling((double)totalStudents / itemsPerPage);

            if (currentPageStudent < totalStudentPages - 1)
            {
                if(totalStudents < studentController.GetAllStudents().Count())
                {
                    currentPageStudent++;
                    UpdateWithPagingStudent(currentPageStudent, itemsPerPage, searchStudents);
                    //MessageBox.Show("uslo");
                }
                else
                {
                    currentPageStudent++;
                    // if(Students.Count <)
                    UpdateWithPaging(currentPageStudent, itemsPerPage);
                    // Update();
                    //UpdateStudentsDataGrid();
                }


            }
        }
        private void NextPageSubject_Click(object sender, RoutedEventArgs e)
        {
            int totalSubjects = subjectCount;
            int totalSubjectPages = (int)Math.Ceiling((double)totalSubjects / itemsPerPage);

            if (currentPageSubject < totalSubjectPages - 1)
            {
                if (totalSubjects < subjectController.GetAllSubjects().Count())
                {
                    currentPageSubject++;
                    UpdateWithPagingSubject(currentPageSubject, itemsPerPage, searchSubjects);
                    //MessageBox.Show("Entered");
                }
                else
                {
                    currentPageSubject++;
                    UpdateWithPaging(currentPageSubject, itemsPerPage);
                }
            }

        }
        private void NextPageProfessor_Click(object sender, RoutedEventArgs e)
        {
            int totalProfessors = professorCount;
            int totalProfessorPages = (int)Math.Ceiling((double)totalProfessors / itemsPerPage);

            if (currentPageProfessor < totalProfessorPages - 1)
            {
                if (totalProfessors < professorController.GetAllProfessors().Count())
                {
                    currentPageProfessor++;
                    UpdateWithPagingProfessor(currentPageProfessor, itemsPerPage, searchProfessors);
                   // MessageBox.Show("Entered");
                }
                else
                {
                    currentPageProfessor++;
                    UpdateWithPaging(currentPageProfessor, itemsPerPage);
                }
            }

        }
        private void NextPageDepartment_Click(object sender, RoutedEventArgs e)
        {
            int totalDepartments = departmentController.GetAllDepartments().Count();
            int totalDepartmentsPages = (int)Math.Ceiling((double)totalDepartments / itemsPerPage);

            if (currentPageDepartment < totalDepartmentsPages - 1)
            {
                currentPageDepartment++;
                UpdateWithPaging(currentPageDepartment, itemsPerPage);
                //UpdateStudentsDataGrid();

            }
        }



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                Add_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                Delete_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
                Click_Save(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C))
                CloseButton_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.X))
                CloseButton_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                  Serbian_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                  English_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                Update_Click(sender, e);
        }
        private List<Student> PagingSort(List<Student> sortedStudents, int currentPage, int itemsPerPage)
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            return sortedStudents.Skip(startIndex).Take(itemsPerPage).ToList();
        }
        private void Student_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true; 
            string columnName = e.Column.SortMemberPath;

            string sortCriteria = "Id";

            
            
                switch (columnName)
                {
                    case "Id":
                        sortCriteria = "Id";
                       // StudentDataGrid.ItemsSource = studentController.GetSortedStudents(currentPageStudent, itemsPerPage, sortCriteria, SortDirection.Descending);
                        break;
                    case "First_Name":
                        sortCriteria = "Name";
                        break;
                    case "Last_Name":
                        sortCriteria = "Last Name";
                        break;
                    case "Current_Year":
                        sortCriteria = "Current year";
                        break;
                    case "Status":
                        sortCriteria = "Status";
                        break;
                    case "Average_Grade":
                        sortCriteria = "Average grade";
                        break;
                }
          

            if (e.Column.SortDirection == ListSortDirection.Ascending)
            {
                StudentDataGrid.ItemsSource = studentController.GetSortedStudents(currentPageStudent, itemsPerPage, sortCriteria, SortDirection.Descending);
                e.Column.SortDirection = ListSortDirection.Descending;
                //UpdateWithPagingStudent(currentPageStudent, itemsPerPage, studentController.GetSortedStudents(currentPageStudent, itemsPerPage, sortCriteria, SortDirection.Descending));
            }
            else
            {
                StudentDataGrid.ItemsSource = studentController.GetSortedStudents(currentPageStudent, itemsPerPage, sortCriteria, SortDirection.Ascending);
                e.Column.SortDirection = ListSortDirection.Ascending;
               // UpdateWithPagingStudent(currentPageStudent, itemsPerPage, studentController.GetSortedStudents(currentPageStudent, itemsPerPage, sortCriteria, SortDirection.Ascending));
            }


           
        }
/*
        public void UpdateWithPagingStudentSort(int page, int itemsPerPage, string sortCriteria, SortDirection sortDirection, List<Student> students)
        {

            Students.Clear();
            foreach (Student student in studentController.GetSortedStudents(page, itemsPerPage, sortCriteria, sortDirection))
            {
                Students.Add(new StudentDTO(student));
            }


        }*/

        // Helper method to get property value by name
        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }



    }

}
