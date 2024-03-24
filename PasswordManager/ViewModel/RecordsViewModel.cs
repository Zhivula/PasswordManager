using PasswordManager.Data;
using PasswordManager.DataBase;
using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModel
{
    class RecordsViewModel : INotifyPropertyChanged
    {
        private List<Account> accounts;
        public List<Account> Accounts
        {
            get => accounts;
            set
            {
                accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }

        readonly RecordsModel model = new RecordsModel();

        public RecordsViewModel(string key)
        {
            Accounts = model.GetAccounts();
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
