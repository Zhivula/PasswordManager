using PasswordManager.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    class AddRecordModel
    {
        public AddRecordModel()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Title">Заголовок - чаще название сайте/ресурса</param>
        /// <param name="Login">Логин</param>
        /// <param name="Password">Пароль</param>
        /// <param name="Comment">Комментарий</param>
        public void AddRecord(string Title, string Login, string Password, string Comment)
        {
            using (var context = new MyDbContext())
            {
                context.Accounts.Add(new Account()
                {
                    Title = Title,
                    Login = Login,
                    Password = Password,
                    Comment = Comment
                });
                context.SaveChanges();
            }
        }
    }
}
