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
        private ObservableCollection<MailFilter> mails;
        public ObservableCollection<MailFilter> Mails
        {
            get => mails;
            set
            {
                mails = value;
                OnPropertyChanged(nameof(Mails));
            }
        }

        readonly MailsModel model = new MailsModel();

        public MailsViewModel(string MasterPassword)
        {
            Mails = new ObservableCollection<MailFilter>();
            var modelmails = model.GetMails();
            if (modelmails.Count() > 0)
            {
                foreach (var i in modelmails)
                {
                    var password = RijndaelManagedEncryption.DecryptText(i.Password, MasterPassword);

                    Mails.Add(new MailFilter
                    {
                        Password = password,
                        PasswordTrue = password,
                        Id = i.Id,
                        Comment = i.Comment,
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
