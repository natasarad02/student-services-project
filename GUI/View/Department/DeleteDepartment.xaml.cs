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
        public MainWindow  mainWindow { get; set; }
        public DeleteDepartment(DepartmentsController departmentController, MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentController = departmentController;
            this.mainWindow = mainWindow;
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
            Closing += Window_Closing;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            departmentController.Delete(department.ToDepartment().Id);
            mainWindow.IsEnabled = true;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWindow.IsEnabled = true;
        }
    }
}
