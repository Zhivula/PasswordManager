using PasswordManager.Data;
using PasswordManager.View;
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
    class MainWindowViewModel : INotifyPropertyChanged
    {

        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public MainWindowViewModel(string key)
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
        public ICommand WindowMaximize => new DelegateCommand(o =>
        {
            window.WindowState = WindowState.Normal;
        });
        public ICommand Add => new DelegateCommand(o =>
        {      
            if (window.ChangedGrid.Children.Count > 0 && window.ChangedGrid.Children[window.ChangedGrid.Children.Count-1].GetType() == new CardsView(string.Empty).GetType())
            {
                window.ChangedGrid.Children.Clear();
                window.ChangedGrid.Children.Add(new AddCardView());
            }
            else
            {
                window.ChangedGrid.Children.Clear();
                window.ChangedGrid.Children.Add(new AddRecordView());
            }
            
        });
        public ICommand AccountsCommand => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new RecordsView(null));
        });
        public ICommand CardsCommand => new DelegateCommand(o =>
        {
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new CardsView(null));
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
