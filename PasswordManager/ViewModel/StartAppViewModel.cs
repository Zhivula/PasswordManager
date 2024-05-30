using PasswordManager.Data;
using PasswordManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
                byte[] SHA512_EN = new byte[0];
                byte[] data = Encoding.UTF8.GetBytes(Password);
                using (SHA512 sha512 = SHA512.Create())
                {
                    SHA512_EN = sha512.ComputeHash(data);
                }

                if (SHA512_EN.Equals((string)Settings.Default["HASH"]))
                {
                    MainWindow mainWindow = new MainWindow(Password);
                    mainWindow.Show();
                    window.Close();
                }
                else
                {
                    IncorrectLabelVisibility = Visibility.Visible;
                }
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
