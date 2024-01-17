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
    public partial class DepartmentSubjectList : Window, IObserver
    {

       // private DepartmentsController departmentsController { get; set; }
        private SubjectsController subjectsController { get; set; }

        public ObservableCollection<SubjectDTO> Subjects { get; set; }


        public DepartmentDTO SelectedDepartment { get; set; }

        
        public UpdateDepartment parentWindow { get; set; }
        public DepartmentSubjectList(SubjectsController subjectsController, DepartmentDTO SelectedDepartment, UpdateDepartment parentWindow)
        {
            InitializeComponent();

            Subjects = new ObservableCollection<SubjectDTO>();
            this.subjectsController = subjectsController;
            subjectsController.Subscribe(this);

            this.SelectedDepartment = SelectedDepartment;
            DataContext = this;
            this.parentWindow = parentWindow;

            Update();
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
            parentWindow.IsEnabled = false;
            Closing += Window_Closing;
        }
        public void Update()
        {

            Subjects.Clear();


            foreach(int professorID in SelectedDepartment.Department_Professors)
            {
                foreach(Subject subject in subjectsController.GetAllSubjects())
                {
                    if(professorID == subject.ProfessorID)
                    {
                        Subjects.Add(new SubjectDTO(subject));
                    }
                }
            }



        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }





    }
}
