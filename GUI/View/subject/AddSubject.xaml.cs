﻿using GUI.DTO;
using StudentskaSluzba.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows;

namespace GUI.View
{
    public partial class AddSubject : Window, INotifyPropertyChanged
    {
        public SubjectDTO Subject { get; set; }
        private SubjectDAO subjectsDAO;
        public event PropertyChangedEventHandler? PropertyChanged;

        public AddSubject(SubjectDAO subjectsDAO)
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            subjectsDAO.AddSubject(Subject.ToSubject());
            Close();
        }
    }

}