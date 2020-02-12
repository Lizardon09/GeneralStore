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
        public float SalePrice { get; set; }
        public int Quantity { get; set; }
        public float TaxPercent { get; set; }
        public DateTime DatePurchased { get; set; }
        public Product()
        {

        }
        public Product(string name, int quantity, float tax)
        {
            ProductName = name;
            Quantity = quantity;
            TaxPercent = tax;
        }

        public Product(string name, int quantity, float tax, float price)
        {
            ProductName = name;
            Quantity = quantity;
            TaxPercent = tax;
            SalePrice = price;
        }

        public abstract bool CanSell();

    }
}
