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
        public List<Mail> GetMails()
        {
            var ListMainPage = new List<Mail>();
            using (var context = new MyDbContext())
            {
                if (context.Mails.Count() > 0)
                {
                    foreach (var item in context.Mails)
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
