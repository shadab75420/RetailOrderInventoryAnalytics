using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<IEnumerable<Product>> GetLowStockProductsAsync();

        Task AddProductAsync(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(Product product);

        Task SaveChangesAsync();
    }
}