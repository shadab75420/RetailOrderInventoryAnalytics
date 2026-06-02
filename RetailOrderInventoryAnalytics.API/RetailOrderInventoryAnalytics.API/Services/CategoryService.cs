using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuditRepository _auditRepository;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IAuditRepository auditRepository)
        {
            _categoryRepository = categoryRepository;
            _auditRepository = auditRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories =
                await _categoryRepository.GetAllCategoriesAsync();

            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            });
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category =
                await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        // US2: Add Category
        public async Task<bool> AddCategoryAsync(CategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            await _categoryRepository.AddCategoryAsync(category);
            await _categoryRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Create",
                "Category",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US2: Update Category
        public async Task<bool> UpdateCategoryAsync(int id, CategoryDto dto)
        {
            var category =
                await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return false;

            category.CategoryName = dto.CategoryName;

            _categoryRepository.UpdateCategory(category);
            await _categoryRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Update",
                "Category",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US2: Delete Category
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category =
                await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return false;

            _categoryRepository.DeleteCategory(category);
            await _categoryRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Delete",
                "Category",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }
    }
}