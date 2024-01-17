﻿using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.DTO;
using System.Runtime.CompilerServices;
using System.Windows;
using CLI.Controller;
namespace GUI.View
{
    public partial class AddStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO Student { get; set;}
        private StudentsController studentController;
        public event PropertyChangedEventHandler ? PropertyChanged;
        public MainWindow mainWindow { get; set; }
        public AddStudent(StudentsController studentController, MainWindow mainWindow) {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentController = studentController;
            this.mainWindow = mainWindow;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            studentController.Add(Student.toStudent());
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

    }
}
