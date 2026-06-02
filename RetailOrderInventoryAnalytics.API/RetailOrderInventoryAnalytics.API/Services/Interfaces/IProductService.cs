using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        Task<ProductDto?> GetProductByIdAsync(int id);

        Task<IEnumerable<ProductDto>> GetLowStockProductsAsync();

        Task<bool> AddProductAsync(ProductDto dto);

        Task<bool> UpdateProductAsync(int id, ProductDto dto);

        Task<bool> DeleteProductAsync(int id);
    }
}