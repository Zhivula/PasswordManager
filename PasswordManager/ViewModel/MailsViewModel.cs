using PasswordManager.Data;
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
    class MailsViewModel
    {
        private ObservableCollection<AccountFilter> accounts;
        public ObservableCollection<AccountFilter> Accounts
        {
            get => accounts;
            set
            {
                accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }

        readonly MailsModel model = new MailsModel();

        public MailsViewModel(string MasterPassword)
        {
            Accounts = new ObservableCollection<AccountFilter>();
            var modelaccounts = model.GetAccounts();
            if (modelaccounts.Count() > 0)
            {
                foreach (var i in modelaccounts)
                {
                    var password = RijndaelManagedEncryption.DecryptText(i.Password, MasterPassword);

                    Accounts.Add(new AccountFilter
                    {
                        Password = password,
                        PasswordTrue = password,
                        Id = i.Id,
                        Comment = i.Comment,
                        Login = i.Login,
                        Title = i.Title
                    });
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
