using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepository = null!;
        private Mock<IAuditRepository> _auditRepository = null!;
        private ProductService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();
            _auditRepository = new Mock<IAuditRepository>();

            _service = new ProductService(
                _productRepository.Object,
                _auditRepository.Object);
        }

        [TestMethod]
        public async Task AddProductAsync_AddsProductSuccessfully()
        {
            // US4: Add Product

            var dto = new ProductDto
            {
                ProductName = "Laptop",
                CategoryId = 1,
                SupplierId = 1,
                UnitPrice = 50000,
                StockQuantity = 10,
                ReorderLevel = 5
            };

            var result = await _service.AddProductAsync(dto);

            Assert.IsTrue(result);

            _productRepository.Verify(
                x => x.AddProductAsync(It.IsAny<Product>()),
                Times.Once);

            _auditRepository.Verify(
                x => x.LogActivityAsync("Create", "Product", "System"),
                Times.Once);
        }

        [TestMethod]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // US4: Get Product By Id

            var product = new Product
            {
                ProductId = 1,
                ProductName = "Laptop",
                CategoryId = 1,
                SupplierId = 1
            };

            _productRepository
                .Setup(x => x.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            var result = await _service.GetProductByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ProductId);
        }
    }
}