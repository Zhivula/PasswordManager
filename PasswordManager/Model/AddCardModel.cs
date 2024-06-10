using PasswordManager.Data;
using PasswordManager.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    class AddCardModel
    {
        public AddCardModel()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bank">Банк</param>
        /// <param name="Number">Номер карты</param>
        /// <param name="Date">Дата действия карты</param>
        /// <param name="Name">Имя пользователя</param>
        public void AddCard(string Bank, string Number, string Date, string MasterPassword, string Name)
        {
            using (var context = new MyDbContext())
            {
                context.Cards.Add(new Card()
                {
                    Bank = Bank,
                    Number = RijndaelManagedEncryption.EncryptText(Number, MasterPassword),
                    Date = Date,
                    Name = Name
                });
                context.SaveChanges();
            }

        }
    }
}
