using System.Text;
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
using GUI.Localization;
using System.Windows.Input;
using System.Linq;
using System;

namespace GUI.View
{
    public partial class StudentList : Window, IObserver
    {

        public ObservableCollection<Student> students { get; set; }

        public ProfessorDTO professor { get; set; }

        private StudentsSubjectsController studentsSubjectsController { get; set; }

        private SubjectsController subjectsController { get; set; }

       
        public UpdateProfessor parentWindow { get; set; }
        //public StudentsController studentsController { get; set; }
        public StudentList(ProfessorDTO pprofessor, SubjectsController subjectsController, UpdateProfessor parentWindow) 
        {
            InitializeComponent();

            students = new ObservableCollection<Student>();
            professor = pprofessor;
            this.studentsSubjectsController = new StudentsSubjectsController();
            this.subjectsController = subjectsController;
            studentsSubjectsController.Subscribe(this);
          //  this.studentsController = studentsController;
            subjectsController.Subscribe(this);

            DataContext = this;
            Update();
            this.parentWindow = parentWindow;
            Left = parentWindow.Left + (parentWindow.Width - Width) / 2;
            Top = parentWindow.Top + (parentWindow.Height - Height) / 2;
            parentWindow.IsEnabled = false;
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

        //za svakog profesora naci sve predmete -> subjectController
        //za svaki predmet naci sve studente -> studentSubjectController
        //info o profesoru
        public void Update() {
            students.Clear();

            foreach (Subject subject in subjectsController.getSubjectsForProfessor(professor.Id)) {

                foreach (Student s in studentsSubjectsController.GetStudents(subject.Id)) 
                {
                    //MessageBox.Show(s.ID.ToString());
                    students.Add(s);
                }

            }

            RemoveDuplicates();

        }

        public void RemoveDuplicates()
        {
            var uniqueItems = new ObservableCollection<Student>();
            foreach (var item in students)
            {
                if (!uniqueItems.Contains(item))
                {
                    uniqueItems.Add(item);
                }
            }

            students = uniqueItems;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }

        public void cancel_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private List<Student> studentSearch(string query)
        {

            string[] words = query.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }

            string index = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (words.Length == 1)
            {
                lastName = words[0];
            }
            else if (words.Length == 2)
            {
                lastName = words[0];
                firstName = words[1];
            }
            else if (words.Length >= 3)
            {

                index = words[0];
                firstName = words[1];
                lastName = string.Join(" ", words.Skip(2));
            }

            ObservableCollection<StudentDTO> studentsTotal = new ObservableCollection<StudentDTO>();
            foreach (Student s in students)
                studentsTotal.Add(new StudentDTO(s));


            var searchResults = studentsTotal.Where(student =>
                                (string.IsNullOrEmpty(index) || student.getIndeks().ToUpper().Contains(index.ToUpper())) &&
                                (string.IsNullOrEmpty(firstName) || student.First_Name.ToUpper().Contains(firstName.ToUpper())) &&
                                (string.IsNullOrEmpty(lastName) || student.Last_Name.ToUpper().Contains(lastName.ToUpper()))
                            ).ToList();

            int totalItems = searchResults.Count;
           
            List<Student> results = new List<Student>();
            foreach (StudentDTO s in searchResults)
                results.Add(s.toStudent());
           
            return results;


        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = txtSearchBox.Text;

            StudentsDataGrid.ItemsSource = studentSearch(query); 
            
                  

            

        }

    }
}
