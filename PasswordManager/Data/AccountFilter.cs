using PasswordManager.DataBase;
using PasswordManager.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PasswordManager.Data
{
    class AccountFilter : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
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
        private string passwordTrue;
        public string PasswordTrue
        {
            get => passwordTrue;
            set
            {
                passwordTrue = value;
                OnPropertyChanged(nameof(PasswordTrue));
            }
        }
        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        private bool clickVision;
        public bool ClickVision
        {
            get => clickVision;
            set
            {
                clickVision = value;
                OnPropertyChanged(nameof(ClickVision));
            }
        }
        private bool clickChange;
        public bool ClickChange
        {
            get => clickChange;
            set
            {
                clickChange = value;
                OnPropertyChanged(nameof(ClickChange));
            }
        }
        private bool clickSave;
        public bool ClickSave
        {
            get => clickSave;
            set
            {
                clickSave = value;
                OnPropertyChanged(nameof(ClickSave));
            }
        }
        private bool isReadOnly;
        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        private string oldTitle;
        private string oldLogin;
        private string oldPassword;

        private SolidColorBrush background;
        public SolidColorBrush Background
        {
            get => background;
            set
            {
                background = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        private SolidColorBrush foreground;
        public SolidColorBrush Foreground
        {
            get => foreground;
            set
            {
                foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }
        private SolidColorBrush foreground_Save;
        public SolidColorBrush Foreground_Save
        {
            get => foreground_Save;
            set
            {
                foreground_Save = value;
                OnPropertyChanged(nameof(Foreground_Save));
            }
        }
        private SolidColorBrush foreground_Change;
        public SolidColorBrush Foreground_Change
        {
            get => foreground_Change;
            set
            {
                foreground_Change = value;
                OnPropertyChanged(nameof(Foreground_Change));
            }
        }
        private SolidColorBrush foreground_Vision;
        public SolidColorBrush Foreground_Vision
        {
            get => foreground_Vision;
            set
            {
                foreground_Vision = value;
                OnPropertyChanged(nameof(Foreground_Vision));
            }
        }

        public DelegateCommand VisionCommand { get; set; }
        public DelegateCommand ChangeCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        public AccountFilter()
        {
            ClickVision = false;
            ClickChange = true;
            clickSave = false;
            IsReadOnly = true;
            Background = new SolidColorBrush(Colors.Transparent);
            Foreground = new SolidColorBrush(Colors.White);
            Foreground_Save = new SolidColorBrush(Colors.Gray);
            Foreground_Change = new SolidColorBrush(Colors.White);
            Foreground_Vision = new SolidColorBrush(Colors.White);
            VisionCommand = new DelegateCommand(o => { Vision(); });
            ChangeCommand = new DelegateCommand(o => { Change(); });
            SaveCommand = new DelegateCommand(o => { Save(); });
        }
        public void Vision()
        {
            if (ClickVision)
            {
                Password = string.Copy(PasswordTrue);
                ClickVision = false;
            }
            else if(!ClickVision && ClickChange)
            {
                Password = new string('*', Password.Length);
                ClickVision = true;
            }
        }
        public void Change()
        {
            if (ClickChange)
            {
                ClickChange = false;
                ClickSave = true;
                ClickVision = true;
                IsReadOnly = false;
                oldLogin = Login;
                oldTitle = Title;
                oldPassword = Password;
                Background = new SolidColorBrush(Colors.SkyBlue);
                Foreground = new SolidColorBrush(Colors.Black);
                Foreground_Change = new SolidColorBrush(Colors.Gray);
                Foreground_Save = new SolidColorBrush(Colors.White);
                Foreground_Vision = new SolidColorBrush(Colors.Gray);
            }
        }
        public void Save()
        {
            if (ClickSave)
            {
                ClickChange = true;
                ClickSave = false;
                ClickVision = false;
                IsReadOnly = true;
                Background = new SolidColorBrush(Colors.Transparent);
                Foreground = new SolidColorBrush(Colors.White);
                Foreground_Change = new SolidColorBrush(Colors.White);
                Foreground_Save = new SolidColorBrush(Colors.Gray);
                Foreground_Vision = new SolidColorBrush(Colors.White);
                Check();
                WindowSuccessfullyViewModel.Successfully();
            }
        }
        private void Check()
        {
            if (!oldTitle.Equals(Title))
            {
                using (var context = new MyDbContext())
                {
                    context.Accounts.Where(x => x.Id == Id).FirstOrDefault().Title = Title;
                    context.SaveChanges();
                }
            }
            if (!oldLogin.Equals(Login))
            {
                using (var context = new MyDbContext())
                {
                    context.Accounts.Where(x => x.Id == Id).FirstOrDefault().Login = Login;
                    context.SaveChanges();
                }
            }
            if (!oldPassword.Equals(Password))
            {
                using (var context = new MyDbContext())
                {
                    context.Accounts.Where(x => x.Id == Id).FirstOrDefault().Password = RijndaelManagedEncryption.EncryptText(Password, "12345678");
                    context.SaveChanges();
                }
            }
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion 
    }
}
