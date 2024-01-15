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

namespace GUI.View
{
    public partial class UpdateDepartment : Window, INotifyPropertyChanged, IObserver
    {
        public DepartmentDTO department { get; set; }
        private DepartmentsController departmentController{ get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        //private List<ProfessorDTO> possibleHOD;

        private List<ProfessorDTO> allProfessors;

        public UpdateDepartment(DepartmentsController departmentController)
        {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentController = departmentController;
            //possibleHOD = new List<ProfessorDTO>();
            allProfessors = new List<ProfessorDTO>();
            //Update();
        }

        public void Update() { 
            allProfessors.Clear();
            foreach (Professor prof in department.Department_Professors)
            {
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
    }
}
