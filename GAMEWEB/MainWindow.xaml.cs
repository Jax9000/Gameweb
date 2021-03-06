﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GAMEWEB {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            var login = new Login();
            login.ShowDialog();
            InitializeComponent();
            if (!User.Connected)
                Close();
            var viewModel = new MainWindowViewModel(TabPanel);
            DataContext = viewModel;
            App.MainWindowManager = viewModel;
            
        }
    }
}
