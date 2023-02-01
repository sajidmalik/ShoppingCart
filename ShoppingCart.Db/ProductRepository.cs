using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Db
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {

            using (ShoppingCartDbContext _context = new ShoppingCartDbContext())
            {
                var products = new List<Product>()
                {
                    new Product { Id = 1, Name = "Soup", UnitPrice = 0.65 },
                    new Product { Id = 2, Name = "Bread", UnitPrice = 0.80 },
                    new Product { Id = 3, Name = "Milk", UnitPrice = 1.15 },
                    new Product { Id = 4, Name = "Apples", UnitPrice = 1.00 }
                };

                if (!_context.Products.Any())
                {
                    _context.Products.AddRange(products);
                    _context.SaveChanges();
                }
            }
        }

        public List<Product> GetAll()
        {
            using (var context = new ShoppingCartDbContext())
            {
                return context.Products.ToList();
            }
        }
    }
}
