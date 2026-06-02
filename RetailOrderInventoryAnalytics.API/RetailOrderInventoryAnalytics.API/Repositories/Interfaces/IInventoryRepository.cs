using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<InventoryTransaction>> GetAllTransactionsAsync();

        Task<IEnumerable<InventoryTransaction>> GetProductTransactionsAsync(int productId);

        Task AddTransactionAsync(InventoryTransaction transaction);

        Task SaveChangesAsync();
    }
}