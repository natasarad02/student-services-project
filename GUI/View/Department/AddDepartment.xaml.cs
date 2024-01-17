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
using CLI.Controller;
namespace GUI.View
{
    public partial class AddDepartment : Window, INotifyPropertyChanged
    {
        public DepartmentDTO department {  get; set; }
        private DepartmentsController departmentController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public MainWindow mainWindow { get; set; }
        public AddDepartment(DepartmentsController departmentController, MainWindow mainWindow) {
            InitializeComponent();
            DataContext = this;
            department = new DepartmentDTO();
            this.departmentController = departmentController;
            this.mainWindow = mainWindow;   
            Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            mainWindow.IsEnabled = false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            departmentController.Add(department.ToDepartment());
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.IsEnabled = true;
            Close();
        }

    }
}
