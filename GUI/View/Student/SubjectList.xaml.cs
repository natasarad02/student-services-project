﻿using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CLI.Controller;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Controls;
using StudentskaSluzba.Model;
using System.Collections.ObjectModel;
using GUI.DTO;
using CLI.Observer;
using System.Collections.Generic;

namespace GUI.View
{
    public partial class SubjectList : Window, IObserver
    {
       
        private StudentsController studentsController { get; set; }
        private StudentsSubjectsController studentsSubjectsController { get; set; }

        public ObservableCollection<SubjectDTO> Subjects { get; set; }
        
        public ObservableCollection<SubjectDTO> attendingSubjects { get; set; }
        public SubjectDTO SelectedSubject { get; set; }

        public StudentDTO Student { get; set; }
        private SubjectsController subjectController { get; set; }
        public SubjectList(StudentDTO Student, StudentsController studentController, StudentsSubjectsController studentsSubjectsController)
        {
            InitializeComponent();
            Subjects = new ObservableCollection<SubjectDTO>();
            subjectController = new SubjectsController();
            subjectController.Subscribe(this);
            studentsController = studentController;

            attendingSubjects = new ObservableCollection<SubjectDTO>();
            this.Student = Student;
            this.studentsSubjectsController = studentsSubjectsController;
            //this.attendingSubjects = attendingSubjects;
            DataContext = this;


            Update();

           
        }
        public void Update()
        {

           Subjects.Clear();
            // bool subjectIsFound = false;

            List<Subject> tmpSubjectList = new List<Subject>();//subjectController.GetAllSubjects();
            foreach (Subject subject in subjectController.GetAllSubjects())
            {
                if (subject.year >= Student.Current_Year)
                {
                    tmpSubjectList.Add(subject);
                }
            }
            foreach (Subject subject in subjectController.GetAllSubjects())
            {


               // MessageBox.Show(subject.Id.ToString());
                foreach (Subject attendingSubject in studentsSubjectsController.GetAllSubjectsByStudent(Student.toStudent()))
                {


                   // MessageBox.Show(attendingSubject.Id.ToString());
                    if(subject.Id == attendingSubject.Id)
                    {

                        //subjectIsFound = true;
                       // MessageBox.Show("Predmet je pronadjen");
                        tmpSubjectList.Remove(subject);
                        break;
                        
                    }



                }        
               
                
            }
            foreach(Subject subject in tmpSubjectList)
                Subjects.Add(new SubjectDTO(subject));



        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {
            
            studentsSubjectsController.Add(Student.Id, SelectedSubject.Id);
            Close();
        }

        
       
    }
}