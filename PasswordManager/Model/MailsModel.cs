using PasswordManager.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    class MailsModel
    {
        public MailsModel()
        {

        }
        public List<Account> GetAccounts()
        {
            var ListMainPage = new List<Account>();
            using (var context = new MyDbContext())
            {
                if (context.Accounts.Count() > 0)
                {
                    foreach (var item in context.Accounts)
                    {
                        ListMainPage.Add(item);
                    }
                    return ListMainPage;
                }
                else return ListMainPage;
            }
        }
    }
}
