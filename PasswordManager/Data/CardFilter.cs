using PasswordManager.DataBase;
using PasswordManager.Model;
using PasswordManager.View;
using PasswordManager.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager.Data
{
    class CardFilter : INotifyPropertyChanged
    {
        public int Id { get; set; }
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
        private string numberTrue;
        public string NumberTrue
        {
            get => numberTrue;
            set
            {
                numberTrue = value;
                OnPropertyChanged(nameof(NumberTrue));
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

        public DelegateCommand VisionCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }

        CardsModel model;

        public CardFilter()
        {
            model = new CardsModel();
            ClickVision = false;
            VisionCommand = new DelegateCommand(o => { Vision(); });
            CloseCommand = new DelegateCommand(o => { Close(); });
        }
        public void Vision()
        {
            if (ClickVision)
            {
                Number = string.Copy(NumberTrue);
                ClickVision = false;
            }
            else
            {
                Number = new string('*', Number.Length);
                ClickVision = true;
            }
        }
        public void Close()
        {
            model.Remove(Id);
            WindowSuccessfullyViewModel.Successfully();
            Update();
        }
        private void Update()
        {
            MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new CardsView(null));
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
