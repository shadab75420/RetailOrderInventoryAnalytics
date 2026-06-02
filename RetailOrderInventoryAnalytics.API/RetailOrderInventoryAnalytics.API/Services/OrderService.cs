using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuditRepository _auditRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IAuditRepository auditRepository)
        {
            _orderRepository = orderRepository;
            _auditRepository = auditRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            return orders.Select(o => new OrderDto
            {
                OrderId = o.Id,
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                UserId = o.UserId
            });
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId
            };
        }

        // US6: Create Order
        public async Task<bool> CreateOrderAsync(OrderDto dto)
        {
            var order = new Order
            {
                OrderNumber = dto.OrderNumber,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.TotalAmount,
                Status = dto.Status,
                UserId = dto.UserId
            };

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Create",
                "Order",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US6: Delete Order
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
                return false;

            _orderRepository.DeleteOrder(order);
            await _orderRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Delete",
                "Order",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }
    }
}