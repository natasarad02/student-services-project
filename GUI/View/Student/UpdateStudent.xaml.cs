﻿using GUI.DTO;
using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CLI.Controller;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;
using CLI.Observer;
using System.Windows.Controls;
namespace GUI.View
{
    public partial class UpdateStudent : Window, INotifyPropertyChanged, IObserver
    {
        public StudentDTO Student { get; set; }
        public SubjectDTO SelectedSubject { get; set; }

        public ExamGradeDTO SelectedGrade { get; set; }
        private bool isUpdate = false;
        private StudentsController studentController;
        private SubjectsController subjectsController;
        private ProfessorsController professorsController;

        private StudentsSubjectsController studentSubjectsController;
        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        public List<Subject> previousList { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
       
        private ExamGradesController examGradesController { get; set; }

        public ObservableCollection<ExamGradeDTO> Grades { get; set; }
        public ObservableCollection<ProfessorDTO> Professors { get; set; }
        public HashSet<Professor> ProfessorsHashSet;

        public MainWindow mainWindow { get; set; }
        public UpdateStudent(StudentsController studentController, StudentsSubjectsController studentSubjectsController, SubjectsController subjectsController, ProfessorsController professorsController, MainWindow mainWindow)
        {
            InitializeComponent();
            
            DataContext = this;
            Student = new StudentDTO();

           
            //subjectsController = new SubjectsController();
           
            Grades = new ObservableCollection<ExamGradeDTO>();
            this.examGradesController = new ExamGradesController();
            examGradesController.Subscribe(this);

            Professors = new ObservableCollection<ProfessorDTO>();
            ProfessorsHashSet = new HashSet<Professor>();
            this.professorsController = professorsController;
            professorsController.Subscribe(this);

            this.studentController = studentController;
            this.studentSubjectsController = studentSubjectsController;
            
            this.subjectsController = subjectsController;


            Subjects = new ObservableCollection<SubjectDTO>();
            studentSubjectsController.Subscribe(this);
            subjectsController.Subscribe(this);

            //Update();
            TabUpdate.SelectionChanged += TabUpdate_SelectionChanged;

            this.mainWindow = mainWindow;

            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            studentController.Update(Student.toStudent());
  
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Add_Subject(object sender, RoutedEventArgs e)
        {
            SubjectList subjectList = new SubjectList(Student, examGradesController, studentController, studentSubjectsController, subjectsController, this);
            subjectList.attendingSubjects = Subjects;
                        
            subjectList.Show();
        }


        public void Update()
        {
            Subjects.Clear();
            foreach (Subject subject in studentSubjectsController.GetAllSubjectsByStudent(Student.toStudent(), subjectsController))
            {
                if(subject.year <= Student.Current_Year)
                    Subjects.Add(new SubjectDTO(subject));
                else
                    studentSubjectsController.Delete(Student.Id, subject.Id);
            }

            Grades.Clear();
            foreach (ExamGrade examGrade in examGradesController.getGradesForStudent(Student.Id)) 
            {
                Subject subject = subjectsController.findSubjectById(examGrade.subjectID);
                Grades.Add(new ExamGradeDTO(examGrade, subject.Name, subject.Espb, subject.Ids));
            }

           
           
        }



        private void TabUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabUpdate.SelectedItem is TabItem selectedTab)
            {
                int tabIndex = TabUpdate.SelectedIndex;

               

                if (isUpdate == false)
                {
                    if (tabIndex == 1 || tabIndex == 2 || tabIndex == 3)
                    {
                        Update();
                        isUpdate = true;
                    }
                }

            }
        }


        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Delete_Subject_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedSubject == null)
            {
                MessageBox.Show("Please select subject for deleting");
            }
            else
            {
                DeleteSubjectFromStudent deleteSubject = new DeleteSubjectFromStudent(studentSubjectsController, Subjects, Student, SelectedSubject, this);
                deleteSubject.Show();
               // studentSubjectsController.Delete(Student.Id, SelectedSubject.Id);
                //Subjects.Remove(SelectedSubject);
            }
            
           
        }

        public void Delete_Grade_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGrade == null) {
                MessageBox.Show("Please choose a grade to delete");
            }
            else
            {
                DeleteGrade deleteGrade = new DeleteGrade(examGradesController, this);
                deleteGrade.exam = SelectedGrade;
                deleteGrade.Show();
                Update();
            }

        }

        public void Pass_Subject_Click(object sender, RoutedEventArgs e) {
            if (SelectedSubject == null)
            {
                MessageBox.Show("Please choose a subject in order to pass this");
            }
            else
            {
                GradeStudent gradeStudent = new GradeStudent(examGradesController, SelectedSubject, Student, Subjects, studentSubjectsController, this); 
                gradeStudent.Show();
            }
        }

        public void Show_Professors_Click(object sender, RoutedEventArgs e)
        {
            StudentProfessorList professorList = new StudentProfessorList(Student, studentSubjectsController, professorsController, subjectsController, this);
            professorList.Show();
        }

    }

}
