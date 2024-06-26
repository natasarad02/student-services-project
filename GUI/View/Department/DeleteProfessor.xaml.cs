﻿using CLI.Controller;
using GUI.DTO;
using GUI.Localization;
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
using System.Windows.Input;

namespace GUI.View;

public partial class DeleteProfessorFromDepartment : Window, INotifyPropertyChanged
{
  //  public ProfessorDTO Professor { get; set; }
   // private ProfessorsController professorController;
    public ProfessorDTO SelectedProfessor { get; set; }
    public DepartmentDTO department { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ObservableCollection<ProfessorDTO> Professors { get; set; }

    public UpdateDepartment parentWindow { get; set; }
    public DeleteProfessorFromDepartment(ProfessorDTO SelectedProfessor, DepartmentDTO department, ObservableCollection<ProfessorDTO> Professors, UpdateDepartment parentWindow)
    {
        InitializeComponent();
        //DataContext = this;
        this.SelectedProfessor = SelectedProfessor;
        this.department = department;
        this.Professors = Professors;
        this.parentWindow = parentWindow;
        Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
        Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
        parentWindow.IsEnabled = false;
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
        department.Department_Professors.Remove(SelectedProfessor.Id);
        Professors.Remove(SelectedProfessor);
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
