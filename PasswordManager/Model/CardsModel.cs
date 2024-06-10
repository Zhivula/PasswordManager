using PasswordManager.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Model
{
    class CardsModel
    {
        public CardsModel()
        {

        }
        public List<Card> GetCards()
        {
            var ListMainPage = new List<Card>();
            using (var context = new MyDbContext())
            {
                if (context.Cards.Count() > 0)
                {
                    foreach (var item in context.Cards)
                    {
                        ListMainPage.Add(item);
                    }
                    return ListMainPage;
                }
                else return ListMainPage;
            }
        }
        public void Remove(int Id)
        {
            using (var context = new MyDbContext())
            {
                if (context.Cards.Count() > 0)
                {
                    var item = context.Cards.Where(x => x.Id == Id).FirstOrDefault();
                    if (item != null) context.Cards.Remove(item);
                    context.SaveChanges();
                }

            }
        }
    }
}
