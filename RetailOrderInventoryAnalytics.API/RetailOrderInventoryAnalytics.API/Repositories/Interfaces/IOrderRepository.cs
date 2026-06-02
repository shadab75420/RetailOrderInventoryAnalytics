using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int id);

        Task AddOrderAsync(Order order);

        void UpdateOrder(Order order);

        void DeleteOrder(Order order);

        Task SaveChangesAsync();
    }
}