using PasswordManager.Data;
using PasswordManager.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    class AddMailModel
    {
        public AddMailModel()
        {

        }
        public void AddMail(string Title, string Password, string MasterPassword, string Comment)
        {
            using (var context = new MyDbContext())
            {
                context.Mails.Add(new Mail()
                {
                    Title = Title,
                    Password = RijndaelManagedEncryption.EncryptText(Password, MasterPassword),
                    Comment = Comment
                });
                context.SaveChanges();
            }

        }
    }
}