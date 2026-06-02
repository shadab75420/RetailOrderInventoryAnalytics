using RetailOrderInventoryAnalytics.API.Models.ViewModels;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<SalesReportViewModel> GetSalesReportAsync();

        Task<IEnumerable<InventoryReportViewModel>> GetInventoryReportAsync();
    }
}