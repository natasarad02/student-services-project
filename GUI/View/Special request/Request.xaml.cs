using CLI.Controller;
using CLI.Observer;
using GUI.DTO;
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

        public Request(SubjectDTO SelSubj1, SubjectDTO SelSubj2)
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

            DataContext = this;

            Update();        
        }


        public void Update()
        {
            Students_Attending_Both.Clear();
            //students attending first
            foreach (Student student in studentsSubjectsController.GetStudents(Subject1.Id)) { 
                Students_Attending_Both.Add(new StudentDTO(student));
            }

            //students attending first
            foreach (Student student in studentsSubjectsController.GetStudents(Subject2.Id))
            {
                Students_Attending_Both.Add(new StudentDTO(student));
            }

            RemoveDuplicates(Students_Attending_Both); 
            
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
            in_both = (List<Student>)students_passed.Intersect(students_didnt_pass);
            foreach (Student student3 in in_both)
            {
                Students_Passed_One_Other_Didnt.Add(new StudentDTO(student3));
            }

        }

        public void RemoveDuplicates(ObservableCollection<StudentDTO> name)
        {
            var uniqueItems = new ObservableCollection<StudentDTO>();
            foreach (var item in name)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            name = uniqueItems;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
