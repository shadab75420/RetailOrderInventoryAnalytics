using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class InventoryServiceTests
    {
        private Mock<IInventoryRepository> _inventoryRepository = null!;
        private InventoryService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _inventoryRepository =
                new Mock<IInventoryRepository>();

            _service =
                new InventoryService(
                    _inventoryRepository.Object);
        }

        [TestMethod]
        public async Task AddInventoryTransactionAsync_AddsTransactionSuccessfully()
        {
            // US5: Add Inventory Transaction

            var dto = new InventoryTransactionDto
            {
                ProductId = 1,
                TransactionType = "IN",
                Quantity = 50
            };

            var result =
                await _service.AddTransactionAsync(dto);

            Assert.IsTrue(result);

            _inventoryRepository.Verify(
                x => x.AddTransactionAsync(
                    It.IsAny<InventoryTransaction>()),
                Times.Once);
        }
    }
}