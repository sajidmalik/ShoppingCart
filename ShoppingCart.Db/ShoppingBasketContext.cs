using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Db
{
    public class ShoppingBasketContext : IShoppingBasksetContext
    {
        private readonly IProductRepository _productRepository;

        private static Dictionary<string, List<CartProduct>> _baskets;

        private Dictionary<string, List<CartProduct>> Baskets =>
           _baskets ?? (_baskets = new Dictionary<string, List<CartProduct>>());

        public Dictionary<string, List<CartProduct>> Basket { get => this.Baskets; set { } }

        public ShoppingBasketContext(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(string cartName, int productId, int quantity)
        {
            var availableProduct = _productRepository.GetAll().Where(p => p.Id == productId).First();

            if (availableProduct == null) return;

            if (this.Baskets.ContainsKey(cartName))
            {
                var existingProduct = this.Baskets[cartName].FirstOrDefault(e => e.Id == productId);

                if (existingProduct != null)
                    this.Baskets[cartName].Remove(existingProduct);

                var cartProduct = CreateCartProduct(availableProduct, quantity);
                this.Baskets[cartName].Add(cartProduct);
            }
            else
            {
                var cartProduct = CreateCartProduct(availableProduct, quantity);
                this.Baskets.Add(cartName, new List<CartProduct>() { cartProduct });
            }
        }

        public void RemoveProduct(string cartName, int productId)
        {
            if (!this.Baskets.ContainsKey(cartName)) return;

            var availableProduct = this.Baskets[cartName].FirstOrDefault(p => p.Id == productId);

            this.Baskets[cartName].Remove(availableProduct);
        }

        private CartProduct CreateCartProduct(Product product, int quantity)
        {
            CartProduct cartProduct = new CartProduct
            {
                Id = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Quantity = quantity
            };
            return cartProduct;
        }

        public List<CartProduct> GetShoppingBasket(string cartName)
        {
            return !this.Baskets.ContainsKey(cartName) ? null : this.Baskets[cartName];
        }
    }
}
