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
    public class ProductsControllerTests
    {
        private Fixture _fixture;

        private ProductsController _productsController;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<ICurrencyConversionService> _mockCurrencyConversionService;

        [OneTimeSetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _mockCurrencyConversionService = new Mock<ICurrencyConversionService>();
            _mockProductRepository = new Mock<IProductRepository>();

            _productsController = new
                ProductsController(_mockCurrencyConversionService.Object,
                _mockProductRepository.Object);
        }

        [Test]
        public void Get_CanReturn_Single_Product()
        {
            // Arrange
            var _expectedProduct = _fixture.Create<Product>();
            _mockProductRepository
                .Setup(e => e.GetAll())
                .Returns(new List<Product> { _expectedProduct });

            // Act
            var result = _productsController.Get("EUR");

            // Assert
            Assert.IsNotNull(_expectedProduct);
            Assert.IsTrue(_expectedProduct.Id == result.Result.FirstOrDefault().Id);
            Assert.IsTrue(_expectedProduct.Name == result.Result.FirstOrDefault().Name);
        }

        [Test]
        public void Get_CanReturn_Products()
        {
            // Arrange
            var _expectedProduct = _fixture.Create<List<Product>>();
            _mockProductRepository
                .Setup(e => e.GetAll())
                .Returns(_expectedProduct);

            // Act
            var result = _productsController.Get(_fixture.Create<string>());

            // Assert
            Assert.IsNotNull(_expectedProduct);
            Assert.AreEqual(_expectedProduct.Count(), result.Result.Count());
        }
    }
}