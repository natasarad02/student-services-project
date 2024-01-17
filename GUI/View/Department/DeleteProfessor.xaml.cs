using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GUI.View;

public partial class DeleteProfessorFromDepartment : Window, INotifyPropertyChanged
{
  //  public ProfessorDTO Professor { get; set; }
   // private ProfessorsController professorController;
    public ProfessorDTO SelectedProfessor { get; set; }
    public DepartmentDTO department { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<ProfessorDTO> Professors { get; set; }
    public DeleteProfessorFromDepartment(ProfessorDTO SelectedProfessor, DepartmentDTO department, ObservableCollection<ProfessorDTO> Professors, UpdateDepartment parentWindow)
    {
        InitializeComponent();
        //DataContext = this;
        this.SelectedProfessor = SelectedProfessor;
        this.department = department;
        this.Professors = Professors;

        Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
        Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Yes_Click(object sender, RoutedEventArgs e)
    {
        department.Department_Professors.Remove(SelectedProfessor.Id);
        Professors.Remove(SelectedProfessor);
        Close();
    }
    private void No_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
