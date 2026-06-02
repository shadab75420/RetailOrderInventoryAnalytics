using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class InventoryApiService
{
    private readonly ApiService _apiService;

    public InventoryApiService(ApiService apiService) => _apiService = apiService;

    public async Task<List<InventoryTransactionViewModel>> GetAllAsync() => await _apiService.GetAsync<List<InventoryTransactionViewModel>>("Inventory") ?? new();
    public async Task<List<InventoryTransactionViewModel>> GetByProductAsync(int productId) => await _apiService.GetAsync<List<InventoryTransactionViewModel>>($"Inventory/{productId}") ?? new();
}
