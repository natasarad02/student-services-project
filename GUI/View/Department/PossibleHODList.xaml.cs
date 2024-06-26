﻿using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
using GUI.Localization;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.View
{
    public partial class PossibleHODList : Window, IObserver
    {

        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public DepartmentDTO Department { get; set; }

        private DepartmentsController departmentsController;
        private ProfessorsController professorsController;

        public ProfessorDTO SelectedProfessor { set; get; }
        public UpdateDepartment parentWindow { get; set; }
        public PossibleHODList(DepartmentsController dep, DepartmentDTO CurrentDepartment, UpdateDepartment parentWindow) 
        {
            InitializeComponent();
            
            Professors = new ObservableCollection<ProfessorDTO>();
            Department = CurrentDepartment;

            departmentsController = dep;
            departmentsController.Subscribe(this);

            professorsController = new ProfessorsController();
            professorsController.Subscribe(this);
            
            DataContext = this;
            Update();
            //centriranje + disable
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
        public void Update()
        {
            Professors.Clear();
            
            foreach (Professor professor in departmentsController.GetProfessorsThatCouldBeHOD(Department.Id, professorsController)) 
            {
                Professors.Add(new ProfessorDTO(professor));
            }
        }

        public void Add_HOD_Click(object sender, RoutedEventArgs e) 
        {
            if (SelectedProfessor == null)
            {
                MessageBox.Show("Please select a professor!");
            }
            else 
            {
                Department.Hod = SelectedProfessor.Name + " " + SelectedProfessor.Surname;
                Department.Hod_id = SelectedProfessor.Id;
                
                departmentsController.Update(Department.ToDepartment()); //izmeniti DAO
                Close();
            }
        }

        public void Cancel_Click(object sender, RoutedEventArgs e) 
        {
            parentWindow.IsEnabled = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }

    }
}
