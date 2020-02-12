using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralStore
{
    public class SupplyProduct
    {
        public Product product { get; set; }
        public float CostPrice { get; set; }

        public SupplyProduct(Product item, float costprice)
        {
            product = item;
            CostPrice = costprice;
        }
    }
}
