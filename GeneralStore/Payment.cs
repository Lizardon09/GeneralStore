using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public enum PayMentOption
    {
        Card,
        Cash,
        PayPal
    }
   public class Payment
    {
        public Customer CustomerP { get; set; }
        public PayMentOption PayMethod { get; set; }
        public float Amount { get; set; }
        public float Change { get; set; }
        public List<Product> ProductsPayedFor { get; set; }

        public Payment(List<Product> products,PayMentOption payMentOption,float amount, Customer customer, float change)
        {
            CustomerP = customer;
            ProductsPayedFor = products;
            PayMethod = payMentOption;
            Amount = amount;
            Change = change;
        }
        public Payment(Product product, PayMentOption payMentOption, float amount, Customer customer, float change)
        {
            CustomerP = customer;
            ProductsPayedFor = new List<Product>();
            ProductsPayedFor.Add(product);
            PayMethod = payMentOption;
            Amount = amount;
            Change = change;
        }

        public void DisplayPaymentInfo()
        {
            Console.WriteLine($"\n\n------Payment info:------" +
                              $"\n\nHolder: {CustomerP.Name}" +
                              $"\nPayment Method: {CustomerP.PayMethod}" +
                              $"\nAmount Payed: R{Amount}" +
                              $"\nChange: R{Change}" +
                              $"\nProducts baught:");

            foreach(var item in ProductsPayedFor)
            {
                item.DisplayProduct();
                Console.WriteLine();
            }

        }
    }
}
