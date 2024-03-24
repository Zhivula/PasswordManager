using PasswordManager.Data;
using PasswordManager.Model;
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
    class AddRecordViewModel : INotifyPropertyChanged
    {
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

        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        readonly AddRecordModel model = new AddRecordModel();

        public AddRecordViewModel()
        {

        }
        public ICommand Save => new DelegateCommand(o =>
        {
            if (!string.IsNullOrWhiteSpace(Password) || !string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Login))
            {
                model.AddRecord(Title, Login, Password, Comment);
                Password = Title = Login = Comment = string.Empty;
                WindowSuccessfullyViewModel.Successfully();
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
