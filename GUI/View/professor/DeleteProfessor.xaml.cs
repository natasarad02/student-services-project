using CLI.Controller;
using GUI.DTO;
using GUI.Localization;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
