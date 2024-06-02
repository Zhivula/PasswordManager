using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordManager.Data
{
    class AccountFilter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordTrue { get; set; }
        public string Comment { get; set; }
        public bool Click { get; set; }
        public DelegateCommand Command { get; set; }
        public AccountFilter()
        {
            Click = false;
            Command = new DelegateCommand(o => { Method(); });
        }
        public void Method()
        {
            if (Click)
            {
                Password = string.Copy(PasswordTrue);
                Click = false;
                MessageBox.Show("Click");
            }
            else
            {
                Password = new string('*', Password.Length);
                Click = true;
                MessageBox.Show("Click");
            }
        }
    }
}
