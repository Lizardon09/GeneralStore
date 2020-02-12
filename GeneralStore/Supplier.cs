using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public class Supplier
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<SupplyProduct> SupplyableProducts { get; set; }

       public Supplier()
        {
            SupplyableProducts = new List<SupplyProduct>() {  new SupplyProduct(new Drink("Heineken",10,0.15F,DrinkType.Alcholic), 200),
                                                                new SupplyProduct(new Drink("Heineken00",10,0.15F,DrinkType.NonAlcoholic), 150)
            };
        }

        public Supplier(string name, string location)
        {
            SupplyableProducts = new List<SupplyProduct>() {  new SupplyProduct(new Drink("Heineken",10,0.15F,DrinkType.Alcholic), 200),
                                                                new SupplyProduct(new Drink("Heineken00",10,0.15F,DrinkType.NonAlcoholic), 150)
            };

            Name = name;
            Location = location;

        }

        public int CheckQuanityOf(Product product)
        {
            foreach(var item in SupplyableProducts)
            {
                if(product.ProductName == item.product.ProductName)
                {
                    return item.product.Quantity;
                }
            }
            return -1;
        }

    }
}
