﻿using PasswordManager.DataBase;
using PasswordManager.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //using (var context = new MyDbContext())
            //{
            //    context.Database.Delete();

            //    context.SaveChanges();
            //}
            MainWindow mainWindow = new MainWindow("12345678");
            mainWindow.Show();





            //var hash = (string)Settings.Default["HASH"];

            //if (string.IsNullOrEmpty(hash))
            //{
            //    FirstStartAppWindow window = new FirstStartAppWindow();
            //    window.Show();
            //}
            //else
            //{
            //    StartAppView window = new StartAppView();
            //    window.Show();
            //}
        }
    }
}
