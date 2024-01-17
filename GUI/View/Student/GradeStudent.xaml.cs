﻿using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GUI.View
{
    public partial class GradeStudent: Window, INotifyPropertyChanged
    {
        public ExamGradeDTO exam { get; set; }
        private ExamGradesController examGradesController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SubjectDTO> Subjects;
        public StudentsSubjectsController StudentsSubjectsController;

        private SubjectDTO Subject { get; set; }
        private UpdateStudent parentWindow { get; set; }

        public GradeStudent(ExamGradesController examGradesController, SubjectDTO Selected_Subject, StudentDTO SelectedStudent,
                            ObservableCollection<SubjectDTO> Subjects, StudentsSubjectsController studentSubjectsController, UpdateStudent parentWindow) { 
            InitializeComponent();
            DataContext = this;
            exam = new ExamGradeDTO();
            this.examGradesController = examGradesController;
            exam.SubjectID = Selected_Subject.Id;
            exam.Subject_ID = Selected_Subject.Ids;
            exam.Name= Selected_Subject.Name;
            exam.StudentID=SelectedStudent.Id;
            this.Subject = Selected_Subject;
            this.StudentsSubjectsController = studentSubjectsController;
            this.Subjects = Subjects;
            this.parentWindow = parentWindow;
            //ispis unutar windowa
            txtID.Text = Selected_Subject.Ids.ToString();
            txtName.Text = Selected_Subject.Name;
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
            parentWindow.IsEnabled = false;
            Closing += Window_Closing;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.IsEnabled = true;
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //brisanje iz subjects
            StudentsSubjectsController.Delete(exam.StudentID, exam.SubjectID);
            Subjects.Remove(Subject);
            //dodavanje ocene
            examGradesController.Add(exam.toExam());
            parentWindow.IsEnabled = true;
            Close();
        }

        private void txtID_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //ne znam sta je ovo
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }
    }
}
