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

namespace GUI.View
{
    public partial class UpdateProfessor : Window, INotifyPropertyChanged, IObserver
    {
        public ProfessorDTO Professor { get; set; }
        private ProfessorsController professorController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<SubjectDTO> MySubjects;
        private SubjectsController subjectsController;
        private SubjectDTO SelectedSubject;


        public UpdateProfessor(ProfessorsController professorController, ProfessorDTO SelectedProfessor)
        {
            InitializeComponent();
            DataContext = this;
            Professor = SelectedProfessor;
            this.professorController = professorController;
            SelectedSubject = new SubjectDTO();
            MySubjects = new ObservableCollection<SubjectDTO>();    
            subjectsController = new SubjectsController();
            subjectsController.Subscribe(this);
            SubjectsDataGrid.ItemsSource = MySubjects;
            Update();

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
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delete_Subject(object sender, RoutedEventArgs e)
        {
            //za selektovani predmet staviti da je prof id == -1 i prof name = null
            //ne radi
            MySubjects.Remove(SelectedSubject);
            SelectedSubject.Id = -1;
            SelectedSubject.ProfessorName = "";
        }
    }

}
