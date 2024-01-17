using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View;

public partial class DeleteProfessor : Window, INotifyPropertyChanged
{
    public ProfessorDTO Professor { get; set; }
    private ProfessorsController professorController { get; set; }
    private DepartmentsController departmentController { get; set; }
    private SubjectsController subjectsController { get; set; }
    //private StudentsController studentsController;
    public event PropertyChangedEventHandler? PropertyChanged;
    public MainWindow mainWindow { get; set; }
    public DeleteProfessor(ProfessorsController professorController, MainWindow mainWindow, SubjectsController subjectsController, DepartmentsController departmentsController)
    {
        InitializeComponent();
        DataContext = this;
        Professor = new ProfessorDTO();
        this.professorController = professorController;
        this.subjectsController = subjectsController;
        this.departmentController = departmentsController;
      //  this.studentsController = studentsController;
        this.mainWindow = mainWindow;
        Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
        Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
        mainWindow.IsEnabled = false;
        Closing += Window_Closing;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Yes_Click(object sender, RoutedEventArgs e)
    {
        foreach (Department department in departmentController.GetAllDepartments())
        {
            foreach (int professorID in department.Department_Professors)
            {
                if (professorID == Professor.Id)
                {
                    department.Department_Professors.Remove(professorID);
                }
            }
        }
        foreach (Subject subject in subjectsController.GetAllSubjects())
        {
            if(Professor.Id == subject.ProfessorID)
            {
                subject.ProfessorID = -1;
                subject.ProfessorName = "";
            }
        }
        
        professorController.Delete(Professor.ToProfessor().Id);
        mainWindow.IsEnabled = true;
        Close();
    }
    private void No_Click(object sender, RoutedEventArgs e)
    {
        mainWindow.IsEnabled = true;
        Close();
    }
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        mainWindow.IsEnabled = true;
    }
}
