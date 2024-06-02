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
    class FirstStartAppViewModel : INotifyPropertyChanged
    {
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        readonly FirstStartAppWindow window = Application.Current.Windows.OfType<FirstStartAppWindow>().FirstOrDefault();
        
        public FirstStartAppViewModel()
        {

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
            if (ConfirmPassword.Equals(Password) && Password.Length > 7)
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] SHA512_EN = new byte[0];
                    byte[] data = Encoding.UTF8.GetBytes(Password);

                    SHA512_EN = sha512.ComputeHash(data); 
                    
                    Settings.Default["HASH"] = SHA512_EN.ToString();
                    Settings.Default.Save();
                }
                MainWindow mainWindow = new MainWindow(Password);
                mainWindow.Show();
                window.Close();
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
