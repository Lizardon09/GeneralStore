using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public enum CustomerType
    {
        Bulk = 20,
        Casual = 100
    }
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Markup { get; set; }
        public CustomerType TypeOfCustomer { get; set; }

        public Customer()
        {

        }

        public Customer(string name, CustomerType type)
        {
            Name = name;
            TypeOfCustomer = type;
        }
    }
}
