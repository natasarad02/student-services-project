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

namespace GUI.View
{
    public partial class SubjectList2 : Window, IObserver
    {

        public ObservableCollection<SubjectDTO> Subjects { get; set; } //subjects without professor

        public SubjectDTO SelectedSubject { get; set; }

        public int professorID { get; set; }

        public string professorName { get; set; }

        public UpdateProfessor parentWindow { get; set; }
        private SubjectsController subjectController { get; set; }
        public SubjectList2(int prof_if, string prof_name, SubjectsController subjectController, UpdateProfessor parentWindow)
        {
            InitializeComponent();
            Subjects = new ObservableCollection<SubjectDTO>();
            this.subjectController = subjectController;
            subjectController.Subscribe(this);
            professorID = prof_if;
            professorName = prof_name;

            DataContext = this;
            this.parentWindow = parentWindow;

            Update();
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
        public void Update()
        {
            Subjects.Clear();

            foreach (Subject subject in subjectController.getSubjectsWithoutProfessor())
            {
                Subjects.Add(new SubjectDTO(subject));
            }
        }

        private void Add_Subject_Click(object sender, RoutedEventArgs e)
        {
            SelectedSubject.ProfessorId = professorID;
            SelectedSubject.ProfessorName = professorName;
            subjectController.Update(SelectedSubject.ToSubject());
            parentWindow.IsEnabled = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) {
            parentWindow.IsEnabled = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            parentWindow.IsEnabled = true;
        }

    }
}
