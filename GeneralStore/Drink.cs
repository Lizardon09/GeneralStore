using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public enum DrinkType
    {
        Alcholic = 15,
        NonAlcoholic = 0
    }
   public class Drink: Product
    {
        public DrinkType TypeOfDrink { get; set; }
        public Drink()
        {

        }

        public Drink(string name, int quantity, DrinkType typeofdrink) : base(name, quantity)
        {
            TypeOfDrink = typeofdrink;
            TaxPercent = (float)TypeOfDrink / 100;
        }

        public Drink(string name, int quantity, DrinkType typeofdrink, float price) : base(name, quantity, price)
        {
            TypeOfDrink = typeofdrink;
            TaxPercent = (float)TypeOfDrink / 100;
        }

        public override bool CanSell()
        {
            return true;
        }

        public override void DisplayProduct()
        {
            base.DisplayProduct();
            Console.WriteLine($"Type of Drink: {(DrinkType)TypeOfDrink}\n");
        }
    }
}
