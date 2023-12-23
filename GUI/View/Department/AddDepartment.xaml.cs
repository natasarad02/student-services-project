using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.DTO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GUI.View.Department
{
    internal class AddDepartment : MainWindow, INotifyPropertyChanged
    {
        public DepartmentDTO department {  get; set; }
        private DepartmentDAO departmentDAO { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AddDepartment(DepartmentDAO departmentDAO) {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentDAO = departmentDAO;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            departmentDAO.AddDepartment(department.ToDepartment());
            Close();
        }

    }
}
