using PasswordManager.Data;
using PasswordManager.Model;
using PasswordManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.ViewModel
{
    class CardsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<CardFilter> cards;
        public ObservableCollection<CardFilter> Cards
        {
            get => cards;
            set
            {
                cards = value;
                OnPropertyChanged(nameof(Cards));
            }
        }
        readonly CardsModel model = new CardsModel();

        public CardsViewModel(string MasterPassword)
        {
            Cards = new ObservableCollection<CardFilter>();
            var modelCards = model.GetCards();
            if (modelCards.Count() > 0)
            {
                foreach (var i in modelCards)
                {
                    var number = RijndaelManagedEncryption.DecryptText(i.Number, MasterPassword);

                    Cards.Add(new CardFilter
                    {
                        Number = number,
                        NumberTrue = number,
                        Id = i.Id,
                        Name = i.Name,
                        Date = i.Date,
                        Bank = i.Bank
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
