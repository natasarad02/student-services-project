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
using StudentskaSluzba.Model;

namespace GUI.View
{
    public partial class UpdateSubject : Window, INotifyPropertyChanged, SubjectWindowInterface
    {
        //private SubjectDTO oldSubject;
        public SubjectDTO Subject { get; set; }

        private SubjectsController subjectController;
        private ProfessorsController professorsController { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public UpdateSubject(SubjectDTO Subject, SubjectsController subjectController, ProfessorsController professorsController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            this.Subject = Subject;
            this.subjectController = subjectController;
            this.professorsController = professorsController;
            // oldSubject = existingSubject.Clone();
            if (Subject.ProfessorName.Equals(""))
            { 
                deleteProfessorButton.IsEnabled = false;
                addProfessorButton.IsEnabled = true;
            }
            else
            {
                deleteProfessorButton.IsEnabled = true;
                addProfessorButton.IsEnabled = false;


            }
          

            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            subjectController.Update(Subject.ToSubject());
            subjectController.Save();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
           // Subject.CopyFrom(oldSubject);
            Close();
        }

        private void Add_Professor(object sender, RoutedEventArgs e)
        {
            ProfessorList professorList = new ProfessorList(Subject, subjectController, professorsController, this);
            professorList.Show();
            addProfessorButton.IsEnabled = false;
            deleteProfessorButton.IsEnabled = true;

        }

        private void Delete_Professor(object sender, RoutedEventArgs e)
        {
            
            
            DeleteProfessorFromSubject deleteProfessor = new DeleteProfessorFromSubject(Subject, addProfessorButton, deleteProfessorButton, this);
            deleteProfessor.Show();
            /*Subject.ProfessorId = -1;
            Subject.ProfessorName = "";
            */
          
          /*  addProfessorButton.IsEnabled = true;
            deleteProfessorButton.IsEnabled = false;
            */

            
             
            
            
        }
    }

}
