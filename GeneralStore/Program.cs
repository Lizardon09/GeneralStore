using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    class Program
    {
        static void Main(string[] args)
        {
            StoreLogic Makro = new StoreLogic();

            Supplier ProductMart = new Supplier("ProductMart", "Pinetown");

            //Customer customervarun = new Customer("Varun", CustomerType.Bulk);

            Drink drinkitem = new Drink("Heineken", 10, DrinkType.Alcholic);

            //Food fooditem = new Food("BarOneCake", 8, 0.15F, FoodType.Persishable);

            Makro.ProcessSupplyOrder(ProductMart, drinkitem, 5);

            Makro.OpenStore();

            //Makro.ProcessSale(customervarun, drinkitem, 2);

            //Makro.ProcessSupplyOrder(ProductMart, fooditem, 2);

            Console.ReadKey();

        }
    }
}
