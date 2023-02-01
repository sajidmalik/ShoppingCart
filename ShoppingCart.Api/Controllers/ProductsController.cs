using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.Db;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICurrencyConversionService _currencyConversionService;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            ICurrencyConversionService currencyConversionService,
            IProductRepository productRepository)
        {
            _currencyConversionService = currencyConversionService;
            _productRepository = productRepository;
        }

        // GET: api/<ProdutsController>/currency
        [HttpGet("{name}/get")]
        public async Task<IEnumerable<Product>> Get(string name)
        {
            var products = _productRepository.GetAll();

            if (products == null) return Enumerable.Empty<Product>();
            
            foreach (var product in products)
            {
                product.UnitPrice = 
                    await _currencyConversionService
                            .Convert(name, product.UnitPrice);
            }
            return products;
        }

        // GET api/<ProdutsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productRepository.GetAll().Where(e => e.Id == id).Single();
        }
    }
}
