﻿using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GUI.DTO;
using CLI.Controller;
using System.Runtime.CompilerServices;

namespace GUI.View
{
    public partial class DeleteGrade : Window, INotifyPropertyChanged
    {
        public ExamGradeDTO exam { get; set; }
        private ExamGradesController examGradesController;
        public event PropertyChangedEventHandler? PropertyChanged;
        private StudentsSubjectsController studentsSubjectsController;

        public DeleteGrade(ExamGradesController examGradesController, UpdateStudent parentWindow) { 
            InitializeComponent();
            DataContext = this;
            exam = new ExamGradeDTO();
            this.examGradesController = examGradesController;
            studentsSubjectsController = new StudentsSubjectsController();
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            studentsSubjectsController.Add(exam.StudentID, exam.SubjectID);
            examGradesController.Delete(exam.MyId);
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
