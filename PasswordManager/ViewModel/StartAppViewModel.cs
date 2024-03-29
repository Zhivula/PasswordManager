﻿using PasswordManager.Data;
using PasswordManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PasswordManager.ViewModel
{
    class StartAppViewModel : INotifyPropertyChanged
    {
        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) IncorrectLabelVisibility = Visibility.Hidden;
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private Visibility incorrectLabelVisibility;
        public Visibility IncorrectLabelVisibility
        {
            get => incorrectLabelVisibility;
            set
            {
                incorrectLabelVisibility = value;
                OnPropertyChanged(nameof(IncorrectLabelVisibility));
            }
        }

        readonly StartAppView window = Application.Current.Windows.OfType<StartAppView>().FirstOrDefault();

        public StartAppViewModel()
        {
            IncorrectLabelVisibility = Visibility.Hidden;
        }
        public ICommand WindowClose => new DelegateCommand(o =>
        {
            window.Close();
        });
        public ICommand WindowMinimize => new DelegateCommand(o =>
        {
            window.WindowState = WindowState.Minimized;
        });
        public ICommand OpenMainWindow => new DelegateCommand(o =>
        {
            if (!string.IsNullOrWhiteSpace(Password))
            {
                var SHA512_EN = SHA256.Encrypt(Password, SHA256.passPhrase, SHA256.saltValue, SHA256.hashAlgorithm, SHA256.passwordIterations, SHA256.initVector, SHA256.keySize);

                //if (SHA512_EN.Equals((string)Settings.Default["HASH"]))
                //{
                Settings.Default["HASH"] = SHA512_EN;
                Settings.Default.Save();



                MainWindow mainWindow = new MainWindow(SHA512_EN);
                    mainWindow.Show();
                    window.Close();
                //}
                //else
                //{
                //    IncorrectLabelVisibility = Visibility.Visible;
                //}
            }

        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
