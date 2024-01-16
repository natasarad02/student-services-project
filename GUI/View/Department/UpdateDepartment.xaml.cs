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
        public DepartmentDTO department { get; set; }
        private DepartmentsController departmentController{ get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private bool isUpdate = false;
        private ProfessorsController professorsController { get; set; }
        //private List<ProfessorDTO> possibleHOD;
        public ProfessorDTO SelectedProfessor { get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public List<Professor> tmpProfessorList;
        public UpdateDepartment(DepartmentsController departmentController, ProfessorsController professorsController)
        {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentController = departmentController;
            departmentController.Subscribe(this);
            tmpProfessorList = new List<Professor>();
            this.professorsController = professorsController;
            professorsController.Subscribe(this);
            //Update();

            //possibleHOD = new List<ProfessorDTO>();
            Professors = new ObservableCollection<ProfessorDTO>();
         

            TabUpdateDepartment.SelectionChanged += TabUpdateDepartment_SelectionChanged;
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
             //  MessageBox.Show("dodaje");
               
            }
           


        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            departmentController.Update(department.ToDepartment());
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Professor(object sender, RoutedEventArgs e)
        {
            DepartmentProfessorList professorList = new DepartmentProfessorList(department, professorsController, departmentController, Professors);
            //professorList.attendingSubjects = Subjects;
      
           
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
                department.Department_Professors.Remove(SelectedProfessor.Id);
                Professors.Remove(SelectedProfessor);
               
            }


        }
    }
}
