using CLI.Controller;
using GUI.DTO;
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
    private ProfessorsController professorController;
    public event PropertyChangedEventHandler? PropertyChanged;

    public DeleteProfessor(ProfessorsController professorController, MainWindow mainWindow)
    {
        InitializeComponent();
        DataContext = this;
        Professor = new ProfessorDTO();
        this.professorController = professorController;
        Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
        Top = mainWindow.Top + (mainWindow.Height - Height) / 2;

    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Yes_Click(object sender, RoutedEventArgs e)
    {
        professorController.Delete(Professor.ToProfessor().Id);
        Close();
    }
    private void No_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
