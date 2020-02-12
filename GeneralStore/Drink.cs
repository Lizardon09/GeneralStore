using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public enum DrinkType
    {
        Alcholic,
        NonAlcoholic
    }
   public class Drink: Product
    {
        public DrinkType TypeOfDrink { get; set; }
        public Drink()
        {

        }

        public Drink(string name, int quantity, float tax, DrinkType typeofdrink) : base(name, quantity, tax)
        {
            TypeOfDrink = typeofdrink;
        }

        public Drink(string name, int quantity, float tax, DrinkType typeofdrink, float price) : base(name, quantity, tax, price)
        {
            TypeOfDrink = typeofdrink;
        }

        public override bool CanSell()
        {
            return true;
        }
    }
}
