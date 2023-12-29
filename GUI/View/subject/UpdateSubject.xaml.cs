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
namespace GUI.View
{
    public partial class UpdateSubject : Window, INotifyPropertyChanged
    {
        private SubjectDTO oldSubject;
        public SubjectDTO Subject { get; set; }

        private SubjectsController subjectController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateSubject(SubjectsController subjectController, SubjectDTO existingSubject)
        {
            InitializeComponent();
            DataContext = this;
            this.subjectController = subjectController;
            oldSubject = existingSubject.Clone();
            Subject = existingSubject;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            subjectController.Update(Subject.ToSubject());
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Subject.CopyFrom(oldSubject);
            Close();
        }
    }

}
