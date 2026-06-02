using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<InventoryTransactionDto>> GetAllTransactionsAsync()
        {
            var transactions =
                await _inventoryRepository.GetAllTransactionsAsync();

            return transactions.Select(t => new InventoryTransactionDto
            {
                TransactionId = t.TransactionId,
                ProductId = t.ProductId,
                ProductName = t.Product?.ProductName ?? "",
                TransactionType = t.TransactionType,
                Quantity = t.Quantity,
                TransactionDate = t.TransactionDate
            });
        }

        public async Task<IEnumerable<InventoryTransactionDto>>
            GetProductTransactionsAsync(int productId)
        {
            var transactions =
                await _inventoryRepository.GetProductTransactionsAsync(productId);

            return transactions.Select(t => new InventoryTransactionDto
            {
                TransactionId = t.TransactionId,
                ProductId = t.ProductId,
                ProductName = t.Product?.ProductName ?? "",
                TransactionType = t.TransactionType,
                Quantity = t.Quantity,
                TransactionDate = t.TransactionDate
            });
        }

        // US5: Add Inventory Transaction
        public async Task<bool> AddTransactionAsync(
            InventoryTransactionDto dto)
        {
            var transaction = new InventoryTransaction
            {
                ProductId = dto.ProductId,
                TransactionType = dto.TransactionType,
                Quantity = dto.Quantity
            };

            await _inventoryRepository.AddTransactionAsync(transaction);
            await _inventoryRepository.SaveChangesAsync();

            return true;
        }
    }
}