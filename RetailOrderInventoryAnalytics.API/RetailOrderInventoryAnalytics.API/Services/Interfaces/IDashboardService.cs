using RetailOrderInventoryAnalytics.API.Models.ViewModels;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}