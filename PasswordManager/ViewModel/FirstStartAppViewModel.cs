using PasswordManager.Data;
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
            if (ConfirmPassword.Equals(Password) && Password.Length > 12)
            {
                var SHA512_EN = SHA256.Encrypt(Password, SHA256.passPhrase, SHA256.saltValue, SHA256.hashAlgorithm, SHA256.passwordIterations, SHA256.initVector, SHA256.keySize);
                Settings.Default["HASH"] = SHA512_EN;
                Settings.Default.Save();

                MainWindow mainWindow = new MainWindow(SHA512_EN);
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
