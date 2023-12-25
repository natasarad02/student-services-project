using CLI.Controller;
using GUI.DTO;
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
    public partial class DeleteDepartment: Window, INotifyPropertyChanged
    {
        public DepartmentDTO department { get; set; }
        private DepartmentsController departmentController;
        public event PropertyChangedEventHandler? PropertyChanged;

        public DeleteDepartment(DepartmentsController departmentController)
        {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentController = departmentController;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            departmentController.Delete(department.ToDepartment().Id);
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
