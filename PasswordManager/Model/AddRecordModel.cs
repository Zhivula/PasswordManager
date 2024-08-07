﻿using PasswordManager.Data;
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
        public void AddRecord(string Title, string Login, string Password, string MasterPassword, string Comment)
        {
            using (var context = new MyDbContext())
            {
                context.Accounts.Add(new Account()
                {
                    Title = Title,
                    Login = Login,
                    Password = RijndaelManagedEncryption.EncryptText(Password, MasterPassword),
                    Comment = Comment
                });
                context.SaveChanges();
            }

        }
        public List<string> GetLogins()
        {
            using (var context = new MyDbContext())
            {
                if (context.Accounts.Count() > 0)
                {
                    return context.Accounts.Select(x => x.Login).Distinct().ToList();
                }
                else return null; 
            }
        }
    }
}
