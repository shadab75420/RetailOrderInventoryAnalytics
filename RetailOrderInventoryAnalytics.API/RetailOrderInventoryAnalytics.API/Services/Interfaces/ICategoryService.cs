using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

        Task<CategoryDto?> GetCategoryByIdAsync(int id);

        Task<bool> AddCategoryAsync(CategoryDto dto);

        Task<bool> UpdateCategoryAsync(int id, CategoryDto dto);

        Task<bool> DeleteCategoryAsync(int id);
    }
}