using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.DTO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GUI.View
{
    public partial class AddStudent : Window, INotifyPropertyChanged
    {
        public StudentDTO Student { get; set;}

        private StudentDAO studentDAO { get; set; }

        public event PropertyChangedEventHandler ? PropertyChanged;

        public AddStudent(StudentDAO studentDAO) {
            InitializeComponent();
            DataContext = this;
            Student = new StudentDTO();
            this.studentDAO = studentDAO;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            studentDAO.addStudent(Student.toStudent());
            Close();
        }

    }
}
