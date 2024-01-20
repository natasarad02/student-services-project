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
using System.Windows.Input;
using GUI.Localization;

namespace GUI.View
{
    public partial class UpdateSubject : Window, INotifyPropertyChanged, SubjectWindowInterface
    {
        //private SubjectDTO oldSubject;
        public SubjectDTO Subject { get; set; }

        private SubjectsController subjectController;
        private ProfessorsController professorsController { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow mainWindow { get; set; }  
        public UpdateSubject(SubjectDTO Subject, SubjectsController subjectController, ProfessorsController professorsController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            this.Subject = Subject;
            this.subjectController = subjectController;
            this.professorsController = professorsController;
            this.mainWindow = mainWindow;
            // oldSubject = existingSubject.Clone();
            if (Subject.ProfessorName != null)
            {
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
            } 
            else 
            {
                deleteProfessorButton.IsEnabled = false;
                addProfessorButton.IsEnabled = true;
            }
            
          

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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            subjectController.Update(Subject.ToSubject());
            subjectController.Save();
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Subject.CopyFrom(oldSubject);
            mainWindow.IsEnabled = true;
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }
    }

}
