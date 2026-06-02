using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class OrderApiService
{
    private readonly ApiService _apiService;

    public OrderApiService(ApiService apiService) => _apiService = apiService;

    public async Task<List<OrderViewModel>> GetAllAsync() => await _apiService.GetAsync<List<OrderViewModel>>("Order") ?? new();
    public Task<OrderViewModel?> GetByIdAsync(int id) => _apiService.GetAsync<OrderViewModel>($"Order/{id}");
    public Task<bool> CreateAsync(OrderViewModel model) => _apiService.PostAsync("Order", model);
}
