using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public abstract class Product
    {
        public string ProductName { get; set; }
        public float BasePrice { get; set; }
        public int Quantity { get; set; }
        public float TaxPercent { get; set; }
        public DateTime DatePurchased { get; set; }
        public Product()
        {

        }
        public Product(string name, int quantity)
        {
            ProductName = name;
            Quantity = quantity;
        }

        public Product(string name, int quantity, float price)
        {
            ProductName = name;
            Quantity = quantity;
            BasePrice = price;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public abstract bool CanSell();

        public virtual void DisplayProduct()
        {
            Console.WriteLine($"\n\nProduct Name: {ProductName}" +
                              $"\nQuantity: {Quantity}" +
                              $"\nTaxPercent: {TaxPercent*100}%" +
                              $"\nBasePrice: R{BasePrice}");
        }

    }
}
