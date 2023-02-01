using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Db
{
    public interface IProductRepository
    {
       public List<Product> GetAll();
    }
}
