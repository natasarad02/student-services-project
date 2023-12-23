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
    public partial class UpdateDepartment : Window, INotifyPropertyChanged
    {
        public DepartmentDTO department { get; set; }
        private DepartmentDAO departmentDAO { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public UpdateDepartment(DepartmentDAO departmentDAO)
        {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentDAO = departmentDAO;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            departmentDAO.AddDepartment(department.ToDepartment());
            Close();
        }
    }
}
