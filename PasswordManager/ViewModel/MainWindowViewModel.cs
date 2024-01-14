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

        public MainWindowViewModel()
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
            window.ChangedGrid.Children.Clear();
            window.ChangedGrid.Children.Add(new AddRecordView());
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
