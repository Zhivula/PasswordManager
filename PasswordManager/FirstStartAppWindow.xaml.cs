﻿using PasswordManager.ViewModel;
using System;
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
using System.Windows.Shapes;

namespace PasswordManager
{
    /// <summary>
    /// Логика взаимодействия для FirstStartAppView.xaml
    /// </summary>
    public partial class FirstStartAppWindow : Window
    {
        public FirstStartAppWindow()
        {
            InitializeComponent();
            DataContext = new FirstStartAppViewModel();
        }
    }
}
