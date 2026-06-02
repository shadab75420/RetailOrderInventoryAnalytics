using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class ReportApiService
{
    private readonly ApiService _apiService;

    public ReportApiService(ApiService apiService) => _apiService = apiService;

    public Task<DashboardViewModel?> GetDashboardAsync() => _apiService.GetAsync<DashboardViewModel>("Dashboard");
    public Task<SalesReportViewModel?> GetSalesReportAsync() => _apiService.GetAsync<SalesReportViewModel>("Report/sales");
    public async Task<List<InventoryReportViewModel>> GetInventoryReportAsync() => await _apiService.GetAsync<List<InventoryReportViewModel>>("Report/inventory") ?? new();
}
