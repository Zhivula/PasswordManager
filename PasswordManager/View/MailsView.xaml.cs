using PasswordManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager.View
{
    /// <summary>
    /// Логика взаимодействия для MailsView.xaml
    /// </summary>
    public partial class MailsView : UserControl
    {
        public MailsView(string MasterPassword)
        {
            InitializeComponent();
            DataContext = new MailsViewModel(MasterPassword);
        }
    }
}
