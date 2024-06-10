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
    class AddCardViewModel : INotifyPropertyChanged
    {
        private string bank;
        public string Bank
        {
            get => bank;
            set
            {
                bank = value;
                OnPropertyChanged(nameof(Bank));
            }
        }
        private string number;
        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        private string date;
        public string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
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
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        readonly AddCardModel model = new AddCardModel();

        public AddCardViewModel()
        {

        }

        public ICommand Save => new DelegateCommand(o =>
        {
            if (!string.IsNullOrWhiteSpace(Bank) || !string.IsNullOrWhiteSpace(MasterPassword) || !string.IsNullOrWhiteSpace(Number) || !string.IsNullOrWhiteSpace(Date))
            {
                model.AddCard(Bank, Number, Date, MasterPassword, Name);
                Bank = MasterPassword = Number = Date = Name = string.Empty;
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
