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
using GUI.Localization;
using System.Windows.Input;

namespace GUI.View
{
    public partial class AddSubject : Window, INotifyPropertyChanged, SubjectWindowInterface
    {
        public SubjectDTO Subject { get; set; }
        private SubjectsController subjectController;
        public event PropertyChangedEventHandler? PropertyChanged;
        private ProfessorsController professorsController;
        public MainWindow mainWindow { get; set; }
        public AddSubject(SubjectsController subjectController, ProfessorsController professorsController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            Subject = new SubjectDTO();
            this.subjectController = subjectController;
            this.professorsController = professorsController;
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            this.mainWindow = mainWindow;

           

            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            Closing += Window_Closing;
            addButton.IsEnabled = false;
            Subject.PropertyChanged += Subject_PropertyChanged;
            
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
        private void Subject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Subject.IsValid) { addButton.IsEnabled = true; }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            subjectController.Add(Subject.ToSubject());
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

        private void Add_Professor(object sender, RoutedEventArgs e)
        {
            ProfessorList professorList = new ProfessorList(Subject, subjectController, professorsController, this);
            professorList.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }
    }

}
