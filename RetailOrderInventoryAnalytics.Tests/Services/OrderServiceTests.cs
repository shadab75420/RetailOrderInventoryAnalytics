using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _orderRepository = null!;
        private Mock<IAuditRepository> _auditRepository = null!;
        private OrderService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _orderRepository =
                new Mock<IOrderRepository>();

            _auditRepository =
                new Mock<IAuditRepository>();

            _service =
                new OrderService(
                    _orderRepository.Object,
                    _auditRepository.Object);
        }

        [TestMethod]
        public async Task CreateOrderAsync_CreatesOrderSuccessfully()
        {
            // US6: Create Order

            var dto = new OrderDto
            {
                OrderNumber = "ORD-001",
                OrderDate = DateTime.Now,
                TotalAmount = 1000,
                Status = "Completed",
                UserId = 1
            };

            var result =
                await _service.CreateOrderAsync(dto);

            Assert.IsTrue(result);

            _orderRepository.Verify(
                x => x.AddOrderAsync(It.IsAny<Order>()),
                Times.Once);

            _auditRepository.Verify(
                x => x.LogActivityAsync(
                    "Create",
                    "Order",
                    "System"),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteOrderAsync_RemovesOrderSuccessfully()
        {
            // US6: Delete Order

            var order = new Order
            {
                Id = 1,
                OrderNumber = "ORD-001"
            };

            _orderRepository
                .Setup(x => x.GetOrderByIdAsync(1))
                .ReturnsAsync(order);

            var result =
                await _service.DeleteOrderAsync(1);

            Assert.IsTrue(result);

            _orderRepository.Verify(
                x => x.DeleteOrder(order),
                Times.Once);

            _auditRepository.Verify(
                x => x.LogActivityAsync(
                    "Delete",
                    "Order",
                    "System"),
                Times.Once);
        }
    }
}