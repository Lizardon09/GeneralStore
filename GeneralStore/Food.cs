using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public enum FoodType
    {
        Persishable,
        NonPerishable
    }
    public class Food : Product
    {
        public FoodType TypeOfFood { get; set; }
        public Food()
        {

        }

        public Food(string name, int quantity, float tax, FoodType typeofdrink) : base(name, quantity, tax)
        {
            TypeOfFood = typeofdrink;
        }

        public Food(string name, int quantity, float tax, FoodType typeofdrink, float price) : base(name, quantity, tax, price)
        {
            TypeOfFood = typeofdrink;
        }

        public override bool CanSell()
        {
            if(TypeOfFood == FoodType.Persishable && (DateTime.Now - DatePurchased).TotalDays>7)
            {
                return false;
            }
            return true;
        }
    }
}
