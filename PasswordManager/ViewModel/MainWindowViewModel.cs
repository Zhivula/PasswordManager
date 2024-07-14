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

        private Visibility visibilityEye_MainWindow;
        public Visibility VisibilityEye_MainWindow
        {
            get => visibilityEye_MainWindow;
            set
            {
                visibilityEye_MainWindow = value;
                OnPropertyChanged(nameof(VisibilityEye_MainWindow));
            }
        }

        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        private string masterPassword;

        public MainWindowViewModel(string key)
        {
            masterPassword = key;
            VisibilityEye_MainWindow = Visibility.Visible;
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
            VisibilityEye_MainWindow = Visibility.Hidden;
            if (window.ChangedGrid.Children.Count > 0 && window.ChangedGrid.Children[window.ChangedGrid.Children.Count-1].GetType() == new CardsView(string.Empty).GetType())
            {
                window.ChangedGrid.Children.Clear();
                window.ChangedGrid.Children.Add(new AddCardView());
            }
            else if (window.ChangedGrid.Children.Count > 0 && window.ChangedGrid.Children[window.ChangedGrid.Children.Count - 1].GetType() == new MailsView(string.Empty).GetType())
            {
                window.ChangedGrid.Children.Clear();
                window.ChangedGrid.Children.Add(new AddMailView());
            }
            else
            {
                window.ChangedGrid.Children.Clear();
                window.ChangedGrid.Children.Add(new AddRecordView());
            }
            
        });
        public ICommand AccountsCommand => new DelegateCommand(o =>
        {
            VisibilityEye_MainWindow = Visibility.Visible;
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new RecordsView(masterPassword));
        });
        public ICommand CardsCommand => new DelegateCommand(o =>
        {
            VisibilityEye_MainWindow = Visibility.Hidden;
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new CardsView(masterPassword));
        });
        public ICommand MailsCommand => new DelegateCommand(o =>
        {
            VisibilityEye_MainWindow = Visibility.Hidden;
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new MailsView(masterPassword));
        });
        public ICommand VisionCommand => new DelegateCommand(o =>
        {
            for (int i = window.ChangedGrid.Children.Count - 1; i >= 0; --i)
            {
                var childTypeName = window.ChangedGrid.Children[i].GetType().Name;
                if (childTypeName == nameof(RecordsView))
                {
                    var records = window.ChangedGrid.Children[i] as RecordsView;
                    var data = records.DataContext as RecordsViewModel;

                    foreach (var item in data.Accounts)
                    {
                        item.Vision();
                    }
                }
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
