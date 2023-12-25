﻿using CLI.Controller;
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

    public DeleteProfessor(ProfessorsController professorController)
    {
        InitializeComponent();
        DataContext = this;
        Professor = new ProfessorDTO();
        this.professorController = professorController;

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
}
