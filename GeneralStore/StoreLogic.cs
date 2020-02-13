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
            AvailableProduct = new List<Product>() {  new Drink("Heineken",10,DrinkType.Alcholic, 200),
                 new Drink("Heineken00",25,DrinkType.NonAlcoholic, 100)
            };
            Payments = new List<Payment>();
            Cart = new List<Product>();
        }

        public void OpenStore()
        {
            string input = "false";
            Console.WriteLine("\n------------------WELCOME!!!!------------------");

            do
            {
                Console.WriteLine("\n\nPlease indicate action to be made:");
                Console.WriteLine("\nPlace supply order: order");
                Console.WriteLine("\nPlace sale: sale");
                Console.WriteLine("\nView Payments: payments");
                Console.WriteLine("\nView Stock: stock\n");

                input = Console.ReadLine();
                switch (input)
                {
                    case "payments":
                        ViewPayments();
                        goto case "question";

                    case "stock":
                        ViewStock();
                        goto case "question";

                    case "order":
                        Console.WriteLine("\n\nSupposed to perform supply order here");
                        //
                        goto case "question";

                    case "sale":
                        Customer customer = new Customer();
                        customer = EnterCustomerInfo(input, customer);
                        AddToCart(input, customer);
                        ConfirmSale(input, customer);

                        goto case "question";
                        //
                    default:
                        //
                        Console.WriteLine("\n\nInvalid command, please try again");
                        input = "false";
                        break;

                    case "question":

                        do
                        {
                            Console.WriteLine("\n\nDo you want to continue store? (Y/N)");
                            input = Console.ReadLine();
                            if (input == "Y")
                            {
                                input = "false";
                                break;
                            }
                            else if (input == "N")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\n\nInvalid input, please try again");
                            }
                        } while (true);
                        break;

                }
            } while (input == "false");

            Console.WriteLine("\n------------------Store is closed------------------");
            Console.WriteLine("\n------------------COME AGAIN!!!!------------------");

        }

        public Customer EnterCustomerInfo(string input, Customer customer)
        {
            do
            {
                Console.WriteLine("\n\nPlease enter customer name\n");
                input = Console.ReadLine();
                if (input.All(Char.IsLetter)){

                    customer.Name = input;
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid input, please enter customer name\n");
                }

            } while (true);

            do
            {
                Console.WriteLine("\n\nPlease enter customer type from following:");
                foreach (var name in Enum.GetNames(typeof(CustomerType)))
                {
                    Console.WriteLine("\n"+name);
                }
                Console.WriteLine();
                input = Console.ReadLine();

                if (Enum.IsDefined(typeof(CustomerType), input)){
                    customer.TypeOfCustomer = (CustomerType)Enum.Parse(typeof(CustomerType), input);
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid customer type inputed, please try again.");
                }

            } while (true);

            do
            {
                Console.WriteLine("\n\nPlease enter payment method from following:");
                foreach (var name in Enum.GetNames(typeof(PayMentOption)))
                {
                    Console.WriteLine("\n" + name);
                }
                Console.WriteLine();
                input = Console.ReadLine();

                if (Enum.IsDefined(typeof(PayMentOption), input))
                {
                    customer.PayMethod = (PayMentOption)Enum.Parse(typeof(PayMentOption), input);
                    break;
                }
                else
                {
                    Console.WriteLine("\n\nInvalid payment method specified, please try again.");
                }

            } while (true);

            return customer;
        }

        public void AddToCart(string input, Customer customer)
        {
            Product cartitem;
            int maxquantity;

            Console.WriteLine("\n\nPlease indicate items to be purchased from following (enter -1 to end):\n");
            for (int i = 0; i < AvailableProduct.Count; i++)
            {
                Console.WriteLine($"\n({i}) {AvailableProduct[i].ProductName}: R{CalculateSalePrice(customer, AvailableProduct[i], 1)} ({AvailableProduct[i].Quantity} available)");
            }

            do
            {
                Console.WriteLine("\n\nPlease indicate item to be purchased (enter -1 to end):\n");
                Console.WriteLine();
                input = Console.ReadLine();
                if (input == "-1")
                {
                    break;
                }
                else if(input.All(Char.IsDigit) && Convert.ToInt32(input) < AvailableProduct.Count)
                {
                    cartitem = (Product) AvailableProduct[Convert.ToInt32(input)].Clone();
                    maxquantity = AvailableProduct[Convert.ToInt32(input)].Quantity;
                    
                    if(Cart.Exists(item => item.ProductName == cartitem.ProductName))
                    {
                        maxquantity -= Cart.Find(item => item.ProductName == cartitem.ProductName).Quantity;
                    }

                    Console.WriteLine("\nPlease input quantity:");
                    Console.WriteLine();
                    input = Console.ReadLine();

                    if (input.All(Char.IsDigit) && Convert.ToInt32(input) > 0 && Convert.ToInt32(input) <= maxquantity)
                    {
                        if(Cart.Exists(item => item.ProductName == cartitem.ProductName))
                        {
                            cartitem = Cart.Find(item => item.ProductName == cartitem.ProductName);
                            cartitem.Quantity += Convert.ToInt32(input);
                        }

                        else
                        {
                            cartitem.Quantity = Convert.ToInt32(input);
                            Cart.Add(cartitem);
                        }
                        Console.WriteLine($"\n{Convert.ToInt32(input)} {cartitem.ProductName} has been added to cart");
                    }

                    else
                    {
                        Console.WriteLine("\n\nInvalid quantity inputed or inputing more than is available/left, please try again.");
                    }

                }
                else
                {
                    Console.WriteLine("\n\nInvalid item code inputed, please try again");
                }

            } while (true);
            Console.WriteLine("\n\nCart has been created succesfully.");
        }

        public void ConfirmSale(string input, Customer customer)
        {
            float amountdue = 0;
            Console.WriteLine("\n\nCurrent Cart list:\n");
            foreach (var item in Cart)
            {
                amountdue += CalculateSalePrice(customer, item, item.Quantity);
                Console.WriteLine($"\n{item.ProductName} : R{CalculateSalePrice(customer, item, item.Quantity)} ({item.Quantity})");

            }

            Console.WriteLine($"\n\nTotal amount due is R{amountdue}");

            do
            {
                Console.WriteLine($"\nPlease enter amount to pay:\n");

                Console.WriteLine();
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
                else
                {
                    Console.WriteLine("\n\nInvalid amount inputed or amount payed is less than amount due, please try again.");
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
                                          $"\nUnit Base price: R{item.BasePrice}" +
                                          $"\nUnit Tax amount ({item.TaxPercent*100}% per unit): R{CalculateTaxedAmount(item)}" +
                                          $"\nUnit Considering Tax: R{item.BasePrice + (CalculateTaxedAmount(item))}" +
                                          $"\nUnit Markup ({(int)customer.TypeOfCustomer}%): R{CalculateSalePrice(customer, item, 1) - (item.BasePrice + (CalculateTaxedAmount(item)))}");
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
                                      $"\nUnit Cost Price: R{item.CostPrice}" +
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
                    item.BasePrice = costprice;
                    item.DatePurchased = DateTime.Now.Date;
                    return;
                }
            }

            product.Quantity = quantity;
            product.BasePrice = costprice;

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
            float finalprice = product.BasePrice + CalculateTaxedAmount(product);
            finalprice += (finalprice * ((float)customer.TypeOfCustomer / 100));
            return finalprice * quantity;
        }

        private float CalculateTaxedAmount(Product product)
        {
            return (product.TaxPercent > 0 ? product.BasePrice * product.TaxPercent : product.BasePrice);
        }

        public void ViewPayments()
        {
            Console.WriteLine("\n\nList of stored payments");
            foreach (var payment in Payments)
            {
                payment.DisplayPaymentInfo();
            }
        }

        public void ViewStock()
        {
            Console.WriteLine("\n\nList of products in stock");
            foreach (var item in AvailableProduct)
            {
                item.DisplayProduct();
            }
        }
    }
}
