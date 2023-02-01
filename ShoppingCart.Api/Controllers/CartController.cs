using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Db;
using ShoppingCart.Models;
using ShoppingCart.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IShoppingBasksetContext _shoppingBasksetContext;
        private readonly ICurrencyConversionService _currencyConversionService;
        public CartController(
            IShoppingBasksetContext shoppingBasksetContext,
            ICurrencyConversionService currencyConversionService)
        {
            _shoppingBasksetContext = shoppingBasksetContext;
            _currencyConversionService = currencyConversionService;
        }

        // GET: api/<CartController>/{cartname}
        [HttpGet("{currency}/{name}")]
        public async Task<IEnumerable<CartProduct>> Get(string name, string currency)
        {
            var _basket = _shoppingBasksetContext.GetShoppingBasket(name);

            foreach(var basketProduct in _basket)
            {
                var _price = await _currencyConversionService.Convert(currency, basketProduct.UnitPrice);
                basketProduct.UnitPrice = _price;
            }
            return _basket;
        }

        // PUT api/<CartController>/name/Add/productId/quantity
        [HttpPut("{name}/Add/{productId}/{quantity}")]
        public IActionResult Put(string name, int productId, int quantity)
        {
            _shoppingBasksetContext.AddProduct(name, productId, quantity);
            return Ok();
        }

        [HttpDelete("{name}/{id}")]
        public IActionResult Delete(string name, int id)
        {
            _shoppingBasksetContext.RemoveProduct(name, id);
            return Ok();
        }
    }
}
