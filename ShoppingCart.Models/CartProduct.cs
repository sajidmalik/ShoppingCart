using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class CartProduct : Product
    {
        public int Quantity { get; set; }

        public double LineTotal
        {
            get
            {
                return Quantity * UnitPrice;
            }
        }
    }
}
