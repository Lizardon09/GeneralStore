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
        private List<Payment> Payments { get; set; }
        private List<Product> Cart { get; set; }

        public StoreLogic()
        {
            AvailableProduct = new List<Product>() {  new Drink("Heineken",10,0.15F,DrinkType.Alcholic),
                 new Drink("Heineken00",10,0.15F,DrinkType.NonAlcoholic)
            };
            Payments = new List<Payment>();
            Cart = new List<Product>();
        }

        public void OpenStore()
        {
            string input = "false";
            Console.WriteLine("\n------------------WELCOME!!!!------------------");
            Console.WriteLine("\n\nPlease indicate action to be made:");
            Console.WriteLine("\nPlace supply order: order");
            Console.WriteLine("\nPlace sale: sale");

            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "order":
                        //
                        break;
                    case "sale":
                        Customer customer = new Customer();
                        customer = EnterCustomerInfo(input, customer);
                        AddToCart(input, customer);
                        ConfirmSale(input, customer);
                        //
                        break;
                    default:
                        //
                        input = "false";
                        break;
                }
            } while (input == "false");

        }

        public Customer EnterCustomerInfo(string input, Customer customer)
        {
            do
            {
                Console.WriteLine("\n\nPlease enter customer name");
                input = Console.ReadLine();
                if (input.All(Char.IsLetter)){

                    customer.Name = input;
                    break;
                }

            } while (true);

            do
            {
                Console.WriteLine("\n\nPlease enter customer type from following:");
                foreach (var name in Enum.GetNames(typeof(CustomerType)))
                {
                    Console.WriteLine("\n"+name);
                }
                input = Console.ReadLine();

                if (Enum.IsDefined(typeof(CustomerType), input)){
                    customer.TypeOfCustomer = (CustomerType)Enum.Parse(typeof(CustomerType), input);
                    break;
                }

            } while (true);

            do
            {
                Console.WriteLine("\n\nPlease enter payment method from following:");
                foreach (var name in Enum.GetNames(typeof(PayMentOption)))
                {
                    Console.WriteLine("\n" + name);
                }
                input = Console.ReadLine();

                if (Enum.IsDefined(typeof(PayMentOption), input))
                {
                    customer.PayMethod = (PayMentOption)Enum.Parse(typeof(PayMentOption), input);
                    break;
                }

            } while (true);

            return customer;
        }

        public void AddToCart(string input, Customer customer)
        {
            Product cartitem;

            Console.WriteLine("\n\nPlease indicate items to be purchased from following (enter -1 to end):\n");
            for (int i = 0; i < AvailableProduct.Count; i++)
            {
                Console.WriteLine($"\n({i}) {AvailableProduct[i].ProductName}: R{CalculateSalePrice(customer, AvailableProduct[i], 1)} ({AvailableProduct[i].Quantity} available)");
            }

            do
            {
                input = Console.ReadLine();
                if (input == "-1")
                {
                    break;
                }
                else if(input.All(Char.IsDigit) && Convert.ToInt32(input) < AvailableProduct.Count)
                {
                    cartitem = AvailableProduct[Convert.ToInt32(input)];

                    Console.WriteLine("\nPlease input quantity:");
                    input = Console.ReadLine();

                    if (input.All(Char.IsDigit) && Convert.ToInt32(input) > 0 && Convert.ToInt32(input) <= cartitem.Quantity)
                    {
                        cartitem.Quantity = Convert.ToInt32(input);
                        Cart.Add(cartitem);
                        Console.WriteLine($"\n{cartitem.Quantity} {cartitem.ProductName} has been added to cart");
                        Console.WriteLine("\n\nPlease indicate items to be purchased from following (enter -1 to end):\n");
                    }

                }

            } while (true);

        }

        public void ConfirmSale(string input, Customer customer)
        {
            float amountdue = 0;
            Console.WriteLine("\n\nCurrent Cart list:\n\n");
            foreach (var item in Cart)
            {
                amountdue += CalculateSalePrice(customer, item, item.Quantity);
                Console.WriteLine($"\n{item.ProductName} : R{CalculateSalePrice(customer, item, item.Quantity)} ({item.Quantity})");

            }

            Console.WriteLine($"\n\nTotal amount due is R{amountdue}");

            do
            {
                Console.WriteLine($"\nPlease enter amount to pay:\n");

                input = Console.ReadLine();

                if(input.All(Char.IsDigit) && Convert.ToInt64(input) >= amountdue)
                {
                    foreach(var product in Cart)
                    {
                        ProcessSale(customer, product, product.Quantity);
                    }
                    Payments.Add(new Payment(Cart, customer.PayMethod, amountdue, customer, Convert.ToInt64(input) - amountdue));
                    Payments[Payments.Count - 1].DisplayPaymentInfo();
                    break;
                }

            } while (true);

            Console.WriteLine("\n\nPayment Complete!!!");
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
                        Console.WriteLine($"\nSold {quantity} {item.ProductName} for R{CalculateSalePrice(customer, item, quantity)} to {customer.Name}:" +
                                          $"\nBase price: R{item.SalePrice}" +
                                          $"\nTax amount ({item.TaxPercent*100}%): R{item.SalePrice*item.TaxPercent}" +
                                          $"\nConsidering Tax: R{item.SalePrice + (item.SalePrice * item.TaxPercent)}" +
                                          $"\nMarkup ({(int)customer.TypeOfCustomer}%): R{CalculateSalePrice(customer, item, 1) - (item.SalePrice + (item.SalePrice * item.TaxPercent))}");
                        return;
                    }

                    else
                    {
                        Console.WriteLine($"\nStock is not available for {product.ProductName}.");
                        return;
                    }

                }
            }

            Console.WriteLine($"\nStore does not sell {product.ProductName}.");
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
                    Console.WriteLine($"\nSuccessful supply order:" +
                                      $"\nSupplier: {supplier.Name}" +
                                      $"\nItem: {product.ProductName}" +
                                      $"\nUnit Price: R{item.CostPrice}" +
                                      $"\nQuantity: {quantity}" +
                                      $"\nTotal Cost Price: R{item.CostPrice * quantity}\n");
                    return;
                }
            }
            Console.WriteLine($"\nSupplier {supplier.Name} does not have stock of {product.ProductName}.");
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

        private float CalculateSalePrice(Customer customer, Product product, int quantity)
        {
            float finalprice = product.SalePrice + (product.SalePrice * product.TaxPercent);
            finalprice += (finalprice * ((float)customer.TypeOfCustomer / 100));
            return finalprice * quantity;
        }

        public void ProcessPayments(Customer customer, List<Product> products, float amount, int quantity, PayMentOption payMentOption)
        {
            foreach(var item in products)
            {

            }
        }

        public void ProcessPayment(Customer customer,Product product, float amount,int quantity,PayMentOption payMentOption)
        {

            if (amount >= CalculateSalePrice(customer, product, quantity))
            {
                Payments.Add(new Payment(product, payMentOption, amount, customer, amount - CalculateSalePrice(customer, product, quantity)));
                Console.WriteLine("Payment Proccessed");
            }
           
        }

    }
}
