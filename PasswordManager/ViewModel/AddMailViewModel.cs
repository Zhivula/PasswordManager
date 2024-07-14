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
    class AddMailViewModel : INotifyPropertyChanged
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
        private string masterPassword;
        public string MasterPassword
        {
            get => masterPassword;
            set
            {
                masterPassword = value;
                OnPropertyChanged(nameof(MasterPassword));
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
        readonly AddMailModel model = new AddMailModel();

        public AddMailViewModel()
        {

        }
        public ICommand Save => new DelegateCommand(o =>
        {
            if (!string.IsNullOrWhiteSpace(Password) || !string.IsNullOrWhiteSpace(MasterPassword) || !string.IsNullOrWhiteSpace(Title))
            {
                model.AddMail(Title, Password, MasterPassword, Comment);
                Password = MasterPassword = Title = Comment = string.Empty;
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
