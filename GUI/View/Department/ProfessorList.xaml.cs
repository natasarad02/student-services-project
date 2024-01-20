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
using GUI.Localization;
using System.Windows.Input;

namespace GUI.View
{
    public partial class DepartmentProfessorList : Window, IObserver
    {
       
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public ObservableCollection<ProfessorDTO> departmentProfessors { get; set; }
        public DepartmentDTO Department { get; set; }
        private DepartmentsController departmentsController { get; set; }
        private ProfessorsController professorsController { get; set; }
        public ProfessorDTO SelectedProfessor { get; set; }

        public List<Professor> tmpProfessorList;
        public UpdateDepartment parentWindow { get; set; }
        public DepartmentProfessorList(DepartmentDTO Department, ProfessorsController professorsController, DepartmentsController departmentsController, ObservableCollection<ProfessorDTO> departmentProfessors, UpdateDepartment parentWindow)
        {
            InitializeComponent();

            Professors = new ObservableCollection<ProfessorDTO>();
            this.departmentsController = departmentsController;
            this.professorsController = professorsController;
            this.departmentProfessors = departmentProfessors;
            professorsController.Subscribe(this);

            this.Department = Department;
            DataContext = this;


            Update();
            this.parentWindow = parentWindow;
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
            parentWindow.IsEnabled = false;
            Closing += Window_Closing;


        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                Serbian_Click(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                English_Click(sender, e);

        }
        public void English_Click(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        public void Serbian_Click(object sender, RoutedEventArgs e)
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-RS");
        }
        public void Update()
        {
            Professors.Clear();

            List<Professor> tmpProfessorList = new List<Professor>();
            foreach (Professor professor in professorsController.GetAllProfessors())
            {
                tmpProfessorList.Add(professor);

            }


            foreach (Professor professor in professorsController.GetAllProfessors())
            {
                foreach(ProfessorDTO departmentProfessor in departmentProfessors)
                {
                    if(professor.Id == departmentProfessor.Id)
                    {
                        tmpProfessorList.Remove(professor);
                    }
                }
            }

            foreach (Professor professor in tmpProfessorList)
                Professors.Add(new ProfessorDTO(professor));

        }

        private void Add_Professor_Click(object sender, RoutedEventArgs e)
        {
            departmentsController.addProfessor(Department.Id, SelectedProfessor.Id);
            parentWindow.IsEnabled = true;
            Close();

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.IsEnabled = true;
            Close();
        }

    }
}
