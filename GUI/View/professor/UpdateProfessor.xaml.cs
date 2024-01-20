using GUI.DTO;
using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;
using CLI.Controller;
using System.Collections.ObjectModel;
using CLI.Observer;
using StudentskaSluzba.Model;
using System.Windows.Controls;
using GUI.Localization;
using System.Windows.Input;

namespace GUI.View
{
    public partial class UpdateProfessor : Window, INotifyPropertyChanged, IObserver
    {
        public ProfessorDTO Professor { get; set; }
        private ProfessorsController professorController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SubjectDTO> MySubjects;
        private SubjectsController subjectsController;
        
        
        public SubjectDTO SelectedSubject { get; set; }
        public MainWindow mainWindow { get; set; }
        
        public UpdateProfessor(ProfessorsController professorController, ProfessorDTO SelectedProfessor, MainWindow mainWindow)
        {

            InitializeComponent();
            DataContext = this;
            Professor = SelectedProfessor;
            this.professorController = professorController;
            
            MySubjects = new ObservableCollection<SubjectDTO>();    
            subjectsController = new SubjectsController();
            subjectsController.Subscribe(this);
            SubjectsDataGrid.ItemsSource = MySubjects;
            Update();
            this.mainWindow = mainWindow;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            Closing += Window_Closing;
            updateButton.IsEnabled = true;
            Professor.PropertyChanged += Professor_PropertyChanged;
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
        private void Professor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //MessageBox.Show("menjam polje");
            if (Professor.IsValid) { updateButton.IsEnabled = true; }
            else
                updateButton.IsEnabled = false;
        }
        public void Update() {
            MySubjects.Clear();
            foreach (Subject subject in subjectsController.getSubjectsForProfessor(Professor.Id)) { 
                MySubjects.Add(new SubjectDTO(subject));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            professorController.Update(Professor.ToProfessor());
            mainWindow.IsEnabled = true;
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Delete_Subject(object sender, RoutedEventArgs e)
        {
            //za selektovani predmet staviti da je prof id == -1 i prof name = null

            if (SelectedSubject == null) //stalno je null
            {
                MessageBox.Show("Please select a subject to delete!!!");
            }
            else {

                DeleteSubjectFromProfessor deleteSubject = new DeleteSubjectFromProfessor(subjectsController, MySubjects, Professor, SelectedSubject, this);
                deleteSubject.Show();
               /* SelectedSubject.ProfessorId = -1;
                SelectedSubject.ProfessorName = null;
                subjectsController.Update(SelectedSubject.ToSubject()); //ovo radi, ali da ne menja stalno je zakomentarisano
                MySubjects.Remove(SelectedSubject);*/
            }

        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e) 
        {
            string full_name = Professor.Name + " " + Professor.Surname;
            SubjectList2 subjectList2 = new SubjectList2(Professor.Id, full_name, subjectsController, this);
            subjectList2.Show();
        }

        private void ShowStudents_Click(object sender, RoutedEventArgs e)
        {
            StudentList studentList = new StudentList(Professor, subjectsController, this);
            studentList.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }


    }

}
