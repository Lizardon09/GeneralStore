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
        public List<Product> ProductsPayingFor { get; set; }

        public Payment(List<Product> products,PayMentOption payMentOption,float amount, Customer customer, float change)
        {
            CustomerP = customer;
            ProductsPayingFor = products;
            PayMethod = payMentOption;
            Amount = amount;
            Change = change;
        }
        public Payment(Product product, PayMentOption payMentOption, float amount, Customer customer, float change)
        {
            CustomerP = customer;
            ProductsPayingFor = new List<Product>();
            ProductsPayingFor.Add(product);
            PayMethod = payMentOption;
            Amount = amount;
            Change = change;
        }

        public void DisplayPaymentInfo()
        {
            Console.WriteLine($"\n\nPayment info:" +
                              $"\n\nHolder: {CustomerP.Name}" +
                              $"\nPayment Method: {CustomerP.PayMethod}" +
                              $"\nAmount Payed: R{Amount}" +
                              $"\nChange: R{Change}");
        }
    }
}
