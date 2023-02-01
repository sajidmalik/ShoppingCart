using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Db
{
    public interface IShoppingBasksetContext
    {
        Dictionary<string, List<CartProduct>> Basket { get; set; }

        void AddProduct(string cartName, int productId, int quantity);

        List<CartProduct> GetShoppingBasket(string name);

        void RemoveProduct(string name, int id);
    }
}
