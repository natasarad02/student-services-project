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

        public ObservableCollection<ProfessorDTO> allProfessors;
        public List<Professor> tmpProfessorList;
        public UpdateDepartment(DepartmentsController departmentController, ProfessorsController professorsController)
        {
            InitializeComponent();
      
            department = new DepartmentDTO();
            this.departmentController = departmentController;
            //possibleHOD = new List<ProfessorDTO>();
            allProfessors = new ObservableCollection<ProfessorDTO>();
            tmpProfessorList = new List<Professor>();
            this.professorsController = professorsController;
            //Update();
            professorsController.Subscribe(this);
            DataContext = this;
            TabUpdate.SelectionChanged += TabUpdate_SelectionChanged;
        }

        private void TabUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabUpdate.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Subjects")
            {
                int tabIndex = TabUpdate.SelectedIndex;



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
            allProfessors.Clear();
            foreach (Professor prof in departmentController.getProfessorsByDepartmentId(department.Id))
            {
                MessageBox.Show(prof.Name);
                allProfessors.Add(new ProfessorDTO(prof));
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
            DepartmentProfessorList professorList = new DepartmentProfessorList(department, professorsController, departmentController);
            //professorList.attendingSubjects = Subjects;
            MessageBox.Show(departmentController.getProfessorsByDepartmentId(department.Id)[0].Name);
           
            professorList.tmpProfessorList = tmpProfessorList;
           
            professorList.Show();
        }

    }
}
