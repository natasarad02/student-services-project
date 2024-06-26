﻿using CLI.Controller;
using CLI.Observer;
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
using System.Windows.Input;

namespace GUI.View
{
    public partial class Request : Window, IObserver
    {
        public ObservableCollection<StudentDTO> Students_Attending_Both;
        public ObservableCollection<StudentDTO> Students_Passed_One_Other_Didnt;
        public SubjectDTO Subject1;
        public SubjectDTO Subject2;

        private StudentsSubjectsController studentsSubjectsController; 
        private StudentsController studentsController;
        private ExamGradesController examGradesController;
        public MainWindow mainWindow { get; set; }
        public Request(SubjectDTO SelSubj1, SubjectDTO SelSubj2, MainWindow mainWindow)
        {
            InitializeComponent();

            Subject1 = SelSubj1;
            Subject2 = SelSubj2;

            Students_Attending_Both = new ObservableCollection<StudentDTO>();
            Students_Passed_One_Other_Didnt = new ObservableCollection<StudentDTO>();

            studentsSubjectsController = new StudentsSubjectsController();
            studentsSubjectsController.Subscribe(this);
            studentsController = new StudentsController();
            studentsController.Subscribe(this);
            examGradesController = new ExamGradesController();
            examGradesController.Subscribe(this);

            txt1.Text = Subject1.Name;
            txt2.Text = Subject2.Name;
            txt3.Text = Subject1.Name;
            txt4.Text = Subject2.Name;

            StudentsDataGrid1.ItemsSource = Students_Attending_Both;
            StudentsDataGrid.ItemsSource = Students_Passed_One_Other_Didnt;

            DataContext = this;


            Update();        

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
        public void Update()
        {
            Students_Attending_Both.Clear();
            
            List<Student> temp = new List<Student>();
            List<Student> temp2 = new List<Student>();
            List<Student> temp3 = new List<Student>();

            //students attending first
            foreach (Student student in studentsSubjectsController.GetStudents(Subject1.Id)) { 
                temp.Add(student);
            }

            //students attending second
            foreach (Student student in studentsSubjectsController.GetStudents(Subject2.Id))
            {
                temp2.Add(student);
            }

            temp3 = GetCommonStudents(temp, temp2);

            foreach(Student studentDTO in temp3)
            {
                Students_Attending_Both.Add(new StudentDTO(studentDTO));
            }

            //-----------------------------------------------------------------------------------------------------

            Students_Passed_One_Other_Didnt.Clear();
            List<Student> students_passed = new List<Student>();
            List<Student> students_didnt_pass = new List<Student>();

            //students that passed subject 1
            foreach(Student student1 in examGradesController.GetStudentsForSubject(Subject1.Id))
            {
                students_passed.Add(student1);
            }

            //students that didnt pass subject 2
            foreach (Student student2 in studentsSubjectsController.GetStudents(Subject2.Id)) 
            {
                students_didnt_pass.Add(student2);
            }
            
            //trazenje preseka
            List<Student> in_both = new List<Student>();
            in_both = GetCommonStudents(students_passed, students_didnt_pass);
            foreach (Student student3 in in_both)
            {
                Students_Passed_One_Other_Didnt.Add(new StudentDTO(student3));
            }

            //MessageBox.Show(Students_Passed_One_Other_Didnt[0].toStudent().ToString());

        }

        public List<Student> GetCommonStudents(List<Student> students_passed, List<Student> students_didnt_pass)
        {
            var commonStudents = new List<Student>();

            foreach (var studentPassed in students_passed)
            {
                foreach (var studentDidntPass in students_didnt_pass)
                {
                    // Check if the student is common to both lists based on Id
                    if (studentPassed.ID == studentDidntPass.ID)
                    {
                        commonStudents.Add(studentPassed);
                        break; // Break the inner loop once a match is found
                    }
                }
            }

            return commonStudents;
        }

        public void Close_Click(object sender, RoutedEventArgs e) 
        {
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }

    }
}
