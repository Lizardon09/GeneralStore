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
   public class Paymenth
    {
        public Customer CustomerP { get; set; }
        public PayMentOption PayMethod { get; set; }
        public float Amount { get; set; }
        public float Change { get; set; }

        public List<Product> ProductsPayingFor { get; set; }

        public Paymenth(List<Product> products,PayMentOption payMentOption,float amount, Customer customer, float change)
        {
            CustomerP = customer;
            ProductsPayingFor = products;
            PayMethod = payMentOption;
            Amount = amount;
            Change = change;
        }
        public Paymenth(Product product, PayMentOption payMentOption, float amount, Customer customer, float change)
        {
            CustomerP = customer;
            ProductsPayingFor.Add(product);
            PayMethod = payMentOption;
            Amount = amount;
            Change = change;
        }
    }
}
