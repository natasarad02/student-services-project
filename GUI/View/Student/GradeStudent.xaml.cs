using CLI.Controller;
using GUI.DTO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View
{
    public partial class GradeStudent: Window, INotifyPropertyChanged
    {
        public ExamGradeDTO exam { get; set; }
        private ExamGradesController examGradesController;
        public event PropertyChangedEventHandler? PropertyChanged;


        public GradeStudent(ExamGradesController examGradesController, SubjectDTO Selected_Subject, StudentDTO SelectedStudent) { 
            InitializeComponent();
            DataContext = this;
            exam = new ExamGradeDTO();
            this.examGradesController = examGradesController;
            exam.SubjectID = Selected_Subject.Id;
            exam.Subject_ID = Selected_Subject.Ids;
            exam.Name= Selected_Subject.Name;
            exam.StudentID=SelectedStudent.Id;


            //ispis unutar windowa
            txtID.Text = Selected_Subject.Ids.ToString();
            txtName.Text = Selected_Subject.Name;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            examGradesController.Add(exam.toExam());
            Close();
        }

    }
}
