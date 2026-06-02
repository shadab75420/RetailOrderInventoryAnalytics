using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class ProductApiService
{
    private readonly ApiService _apiService;

    public ProductApiService(ApiService apiService) => _apiService = apiService;

    public async Task<List<ProductViewModel>> GetAllAsync() => await _apiService.GetAsync<List<ProductViewModel>>("Product") ?? new();
    public Task<ProductViewModel?> GetByIdAsync(int id) => _apiService.GetAsync<ProductViewModel>($"Product/{id}");
    public async Task<List<ProductViewModel>> GetLowStockAsync() => await _apiService.GetAsync<List<ProductViewModel>>("Product/low-stock") ?? new();
    public Task<bool> CreateAsync(ProductViewModel model) => _apiService.PostAsync("Product", model);
    public Task<bool> UpdateAsync(ProductViewModel model) => _apiService.PutAsync($"Product/{model.ProductId}", model);
}
