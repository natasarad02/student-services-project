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
using System.Windows.Controls;

namespace GUI.View;

public partial class DeleteProfessorFromSubject : Window, INotifyPropertyChanged
{
  //  public ProfessorDTO Professor { get; set; }
   // private ProfessorsController professorController;
    public SubjectDTO Subject { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public bool yesClick { get; set; }
    public Button addProfessorButton { get; set; }
    public Button deleteProfessorButton { get; set; }
    public UpdateSubject parentWindow { get; set; }
    public DeleteProfessorFromSubject(SubjectDTO Subject, Button addProfessorButton, Button deleteProfessorButton, UpdateSubject parentWindow)
    {
        InitializeComponent();
        //DataContext = this;
        this.Subject = Subject;
        yesClick = false;
        this.addProfessorButton = addProfessorButton;
        this.deleteProfessorButton = deleteProfessorButton;
        this.parentWindow = parentWindow;
        Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
        Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
        parentWindow.IsEnabled = false;
        Closing += Window_Closing;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Yes_Click(object sender, RoutedEventArgs e)
    {
        yesClick = true;
        Subject.ProfessorId = -1;
        Subject.ProfessorName = "";
        addProfessorButton.IsEnabled = true;
        deleteProfessorButton.IsEnabled = false;
        parentWindow.IsEnabled = true;
        Close();
    }
    private void No_Click(object sender, RoutedEventArgs e)
    {
        parentWindow.IsEnabled = true;
        Close();
    }
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        parentWindow.IsEnabled = true;
    }
}
