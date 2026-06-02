using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class SupplierApiService
{
    private readonly ApiService _apiService;

    public SupplierApiService(ApiService apiService) => _apiService = apiService;

    public async Task<List<SupplierViewModel>> GetAllAsync() => await _apiService.GetAsync<List<SupplierViewModel>>("Supplier") ?? new();
    public Task<SupplierViewModel?> GetByIdAsync(int id) => _apiService.GetAsync<SupplierViewModel>($"Supplier/{id}");
    public Task<bool> CreateAsync(SupplierViewModel model) => _apiService.PostAsync("Supplier", model);
    public Task<bool> UpdateAsync(SupplierViewModel model) => _apiService.PutAsync($"Supplier/{model.SupplierId}", model);
}
