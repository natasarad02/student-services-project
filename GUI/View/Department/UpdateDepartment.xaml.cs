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
using CLI.Observer;
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace GUI.View
{
    public partial class UpdateDepartment : Window, INotifyPropertyChanged, IObserver
    {
        public DepartmentDTO department { get; set; } //selektovani departman
        private DepartmentsController departmentController{ get; set; }

        private SubjectsController subjectsController { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool isUpdate = false;
        private ProfessorsController professorsController { get; set; }
        //private List<ProfessorDTO> possibleHOD;
        public ProfessorDTO SelectedProfessor { get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public List<Professor> tmpProfessorList;
        
        public MainWindow mainWindow { get; set; }
        public UpdateDepartment(DepartmentsController departmentController, ProfessorsController professorsController, SubjectsController subjectsController, 
                                MainWindow mainWindow, DepartmentDTO SelectedDepartment)
        {
            InitializeComponent();
            DataContext = this;
            this.department = SelectedDepartment;
            this.departmentController = departmentController;
            departmentController.Subscribe(this);
            tmpProfessorList = new List<Professor>();
            this.professorsController = professorsController;
            professorsController.Subscribe(this);
            this.subjectsController = subjectsController;
            
            Professors = new ObservableCollection<ProfessorDTO>();


            this.mainWindow = mainWindow;
            TabUpdateDepartment.SelectionChanged += TabUpdateDepartment_SelectionChanged;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            Closing += Window_Closing;

            
            if (SelectedDepartment.Hod != null && SelectedDepartment.Hod.Equals(""))
            {
                deleteProfessorButton.IsEnabled = false;
                addProfessorButton.IsEnabled = true;
            }
            else if (SelectedDepartment.Hod != null && !SelectedDepartment.Hod.Equals(""))
            {
                deleteProfessorButton.IsEnabled = true;
                addProfessorButton.IsEnabled = false;
            }
            else
            {
                deleteProfessorButton.IsEnabled = false;
                addProfessorButton.IsEnabled = true;
            }
            updateButton.IsEnabled = false;
            department.PropertyChanged += Department_PropertyChanged;

        }

        private void Department_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (department.IsValid) { updateButton.IsEnabled = true; }
            else
                updateButton.IsEnabled = false;
        }
        private void TabUpdateDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabUpdateDepartment.SelectedItem is TabItem selectedTab)
            {

                int tabIndex = TabUpdateDepartment.SelectedIndex;
                
                if (isUpdate == false)
                {
                    if (tabIndex == 1)
                    {
                        Update();
                        
                        isUpdate = true;
                    }
                }

             
            }
        }
        public void Update() {
            Professors.Clear();
            
            foreach (Professor prof in departmentController.getProfessorsByDepartmentProfessors(department.ToDepartment(), professorsController))
            {
               Professors.Add(new ProfessorDTO(prof));
                            
            }
            
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            departmentController.Update(department.ToDepartment());
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Add_Professor(object sender, RoutedEventArgs e)
        {
            DepartmentProfessorList professorList = new DepartmentProfessorList(department, professorsController, departmentController, Professors, this);
      
            professorList.Show();
        }

        public void Delete_Professor_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedProfessor == null)
            {
                MessageBox.Show("Please select professor for deleting");
            }
            else
            {
                DeleteProfessorFromDepartment deleteProfessor = new DeleteProfessorFromDepartment(SelectedProfessor, department, Professors, this);
                deleteProfessor.Show();
               /* department.Department_Professors.Remove(SelectedProfessor.Id);
                Professors.Remove(SelectedProfessor);
               */
            }


        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }

        public void Add_HOD(object sender, RoutedEventArgs e) {
            if(department.Department_Professors.Count == 0)
            {
                MessageBox.Show("You need to add professors first");
            }
            else
            {
                PossibleHODList possible = new PossibleHODList(departmentController, department);
                possible.Show();
                Update();
                addProfessorButton.IsEnabled = false;
                deleteProfessorButton.IsEnabled = true;
            }
           
        }

        public void Delete_HOD(object sender, RoutedEventArgs e)
        {
            department.Hod = null;
            department.Hod_id = -1;
            Update();
            deleteProfessorButton.IsEnabled = false;
            addProfessorButton.IsEnabled = true;
        }

        public void Show_Subjects_Click(object sender, RoutedEventArgs e)
        {
            DepartmentSubjectList subjectList = new DepartmentSubjectList(subjectsController, department, this);
            subjectList.Show();
        }




    }
}
