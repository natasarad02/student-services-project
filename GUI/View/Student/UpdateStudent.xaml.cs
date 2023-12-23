using GUI.DTO;
using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.View
{
    public partial class UpdateStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO Student { get; set; }

        private StudentDAO studentDAO { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateStudent(StudentDAO studentDAO)
        {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentDAO = studentDAO;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            studentDAO.addStudent(Student.toStudent());
            Close();
        }
    }
}
