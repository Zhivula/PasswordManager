using PasswordManager.Data;
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
    class AddRecordViewModel : INotifyPropertyChanged
    {
        readonly MainWindow window = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

        public AddRecordViewModel()
        {

        }
        public ICommand Save => new DelegateCommand(o =>
        {

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
