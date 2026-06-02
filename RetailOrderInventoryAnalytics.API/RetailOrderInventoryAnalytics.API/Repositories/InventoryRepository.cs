using Microsoft.EntityFrameworkCore;
using RetailOrderInventoryAnalytics.API.Data;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetAllTransactionsAsync()
        {
            return await _context.InventoryTransactions
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<IEnumerable<InventoryTransaction>> GetProductTransactionsAsync(int productId)
        {
            return await _context.InventoryTransactions
                .Include(x => x.Product)
                .Where(x => x.ProductId == productId)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(InventoryTransaction transaction)
        {
            await _context.InventoryTransactions.AddAsync(transaction);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
