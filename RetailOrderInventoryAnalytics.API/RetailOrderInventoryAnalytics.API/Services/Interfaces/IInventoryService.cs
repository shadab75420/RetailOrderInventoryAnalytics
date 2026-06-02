using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryTransactionDto>> GetAllTransactionsAsync();

        Task<IEnumerable<InventoryTransactionDto>> GetProductTransactionsAsync(int productId);

        Task<bool> AddTransactionAsync(InventoryTransactionDto dto);
    }
}