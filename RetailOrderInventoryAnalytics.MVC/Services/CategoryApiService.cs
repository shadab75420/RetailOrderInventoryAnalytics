using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class CategoryApiService
{
    private readonly ApiService _apiService;

    public CategoryApiService(ApiService apiService) => _apiService = apiService;

    public async Task<List<CategoryViewModel>> GetAllAsync() => await _apiService.GetAsync<List<CategoryViewModel>>("Category") ?? new();
    public Task<CategoryViewModel?> GetByIdAsync(int id) => _apiService.GetAsync<CategoryViewModel>($"Category/{id}");
    public Task<bool> CreateAsync(CategoryViewModel model) => _apiService.PostAsync("Category", model);
    public Task<bool> UpdateAsync(CategoryViewModel model) => _apiService.PutAsync($"Category/{model.CategoryId}", model);
}
