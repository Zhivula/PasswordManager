using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataBase
{
    class Mail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Password { get; set; } //здесь храним зашифрованные пароли
        public string Comment { get; set; }
    }
}
