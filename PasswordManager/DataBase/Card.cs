using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataBase
{
    class Card
    {
        public int Id { get; set; }
        public string Bank { get; set; }
        public string Number { get; set; }
        public string Date { get; set; } 
        public string Name { get; set; }
    }
}
