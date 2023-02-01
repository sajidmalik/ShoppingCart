using AutoFixture;
using Moq;
using NUnit.Framework;
using ShoppingCart.Api.Controllers;
using ShoppingCart.Db;
using ShoppingCart.Models;
using ShoppingCart.Services;

namespace ShoppingCart.Api.Tests
{
    [TestFixture]
    public class CartControllerTests
    {
        private Fixture _fixture;
        private CartController _cartController;

        private Mock<IShoppingBasksetContext> _mockShoppingBasksetContext;
        private Mock<ICurrencyConversionService> _mockCurrencyConversionService;


        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockCurrencyConversionService = new Mock<ICurrencyConversionService>();
            _mockShoppingBasksetContext = new Mock<IShoppingBasksetContext>();

            _cartController = new
                CartController(_mockShoppingBasksetContext.Object,
                _mockCurrencyConversionService.Object);
        }

        [Test]
        public void Get_CanReturn_ShoppingBasket()
        {
            // Arrange
            var _basketProducts = _fixture.CreateMany<CartProduct>().ToList();

            _mockShoppingBasksetContext.SetupProperty(p => p.Basket).
                SetReturnsDefault(_basketProducts);

            _mockShoppingBasksetContext
                .Setup(e => e.GetShoppingBasket(_fixture.Create<string>()))
            .Returns(_basketProducts);

            // Act
            var result = _cartController.Get(_fixture.Create<string>(), _fixture.Create<string>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(_basketProducts.Count(), result.Result.Count());
        }

        [Test]
        public void AddProduct_ShoppingBasketContext_IsExcuted()
        {
            string name = _fixture.Create<string>();
            int productId = _fixture.Create<int>();
            int quantity = _fixture.Create<int>();

            // Arrange
            _mockShoppingBasksetContext.
                Setup(e => e.AddProduct(name, productId, quantity));

            // Act
            _cartController.Put(name, productId, quantity);

            // Assert
            _mockShoppingBasksetContext.Verify(e => e.AddProduct(name, productId, quantity), Times.Once);
        }

    }
}
