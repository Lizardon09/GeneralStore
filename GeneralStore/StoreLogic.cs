using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
   public class StoreLogic
    {
        public List<Product> AvailableProduct { get; set; }
        private List<Paymenth> _paymenths { get; set; }

        public StoreLogic()
        {
            AvailableProduct = new List<Product>() {  new Drink("Heineken",10,0.15F,DrinkType.Alcholic),
                 new Drink("Heineken00",10,0.15F,DrinkType.NonAlcoholic)
            };
        }

        public void ProcessSale(Customer customer, Product product, int quantity)
        {
            foreach(var item in AvailableProduct)
            {
                if(item.ProductName==product.ProductName && item.CanSell())
                {
                    if (quantity <= item.Quantity)
                    {
                        RemoveFromStock(item, quantity);
                        Console.WriteLine($"Sold {item.ProductName} for R{CaclSalePrice(customer, item, quantity)}:\n");
                        return;
                    }

                    else
                    {
                        Console.WriteLine("Stock is not available.");
                        return;
                    }

                }
            }

            Console.WriteLine("Product is unavailable.");
            return;
        }
        public void ProcessSupplyOrder(Supplier supplier, Product product, int quantity)
        {
            foreach (var item in supplier.SupplyableProducts)
            {
                if (product.ProductName == item.product.ProductName && item.product.Quantity >= quantity)
                {
                    item.product.Quantity -= quantity;
                    AddToStock(product, quantity, item.CostPrice);
                    Console.WriteLine($"Successful supply order for {quantity} {product.ProductName} for R{item.CostPrice * quantity}");
                    return;
                }
            }
            Console.WriteLine($"Supplier does not have stock of {product.ProductName}.");
        }

        private void AddToStock(Product product, int quantity, float costprice)
        {
            foreach(var item in AvailableProduct)
            {
                if (item.ProductName == product.ProductName)
                {
                    item.Quantity += quantity;
                    item.SalePrice = costprice;
                    item.DatePurchased = DateTime.Now.Date;
                    return;
                }
            }

            product.Quantity = quantity;
            product.SalePrice = costprice;

            AvailableProduct.Add(product);
        }

        private void RemoveFromStock(Product product, int quantity)
        {
            foreach (var item in AvailableProduct)
            {
                if (item.ProductName == product.ProductName)
                {
                    item.Quantity -= quantity;
                    return;
                }
            }
        }

        private int CheckQuanityOf(Product product)
        {
            foreach (var item in AvailableProduct)
            {
                if (product.ProductName == item.ProductName)
                {
                    return item.Quantity;
                }
            }
            return -1;
        }

        private float CaclSalePrice(Customer customer, Product product, int quantity)
        {
            float finalprice = product.SalePrice + (product.SalePrice * product.TaxPercent);
            finalprice += (finalprice * ((float)customer.TypeOfCustomer / 100));
            return finalprice;
        }

        public void ProcessPayment(Customer customer,Product product, float amount,int quantity,PayMentOption payMentOption)
        {

            if (amount >= CaclSalePrice(customer, product, quantity))
            {
                _paymenths.Add(new Paymenth(product, payMentOption, amount, customer, amount - CaclSalePrice(customer, product, quantity)));
                Console.WriteLine("Payment Proccessed");
            }
           
        }



    }
}
