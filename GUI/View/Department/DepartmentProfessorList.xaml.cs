using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using StudentskaSluzba.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CLI.Controller;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;
using GUI.DTO;
using CLI.Observer;
using System.Collections.Generic;
namespace GUI.View
{
    public partial class DepartmentProfessorList : Window, IObserver
    {
       
        public ObservableCollection<ProfessorDTO> Professors { get; set; }

        public DepartmentDTO Department { get; set; }
        private DepartmentsController departmentsController { get; set; }
        private ProfessorsController professorsController { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }

        public List<Professor> tmpProfessorList;
        public DepartmentProfessorList(DepartmentDTO Department, ProfessorsController professorsController, DepartmentsController departmentsController)
        {
            InitializeComponent();

            Professors = new ObservableCollection<ProfessorDTO>();
            this.departmentsController = departmentsController;
            this.professorsController = professorsController;
            professorsController.Subscribe(this);

            this.Department = Department;
            DataContext = this;


            Update();


        }

        public void Update()
        {

            Professors.Clear();

            foreach (Professor professor in professorsController.GetAllProfessors())
                Professors.Add(new ProfessorDTO(professor));

        }

        private void Add_Professor_Click(object sender, RoutedEventArgs e)
        {
            departmentsController.addProfessor(Department.Id, SelectedProfessor.Id);
            Close();

        }
    }
}
