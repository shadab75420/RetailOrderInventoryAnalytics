using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface IForecastService
    {
        Task<IEnumerable<SalesForecastDto>> GenerateForecastAsync();
    }
}