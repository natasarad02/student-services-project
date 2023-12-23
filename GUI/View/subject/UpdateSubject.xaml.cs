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

namespace GUI.View.Subject
{
    public partial class UpdateSubject : MainWindow, INotifyPropertyChanged
    {
        public SubjectDTO Subject { get; set; }

        private SubjectDAO subjectsDAO;
        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateSubject(SubjectDAO subjectsDAO)
        {
            InitializeComponent();
            DataContext = this;
            Subject = new SubjectDTO();
            this.subjectsDAO = subjectsDAO;

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            subjectsDAO.UpdateSubject(Subject.ToSubject());
            Close();
        }
    }

}
