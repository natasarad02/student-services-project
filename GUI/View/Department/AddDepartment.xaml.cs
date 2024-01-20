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
using StudentskaSluzba.Model;
using GUI.Localization;
using System.Windows.Input;

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
            Closing += Window_Closing;
            addButton.IsEnabled = false;
            department.PropertyChanged += Department_PropertyChanged;
           
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void Department_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (department.IsValid) { addButton.IsEnabled = true; }  
            else
                addButton.IsEnabled = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
                department.Hod_id = -1;
                departmentController.Add(department.ToDepartment());
                mainWindow.IsEnabled = true;

                Close();

          
           
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
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
