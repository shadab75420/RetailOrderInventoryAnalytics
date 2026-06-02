using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IProductRepository _productRepository;

        public ForecastService(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // US12: Sales Forecasting
        public async Task<IEnumerable<SalesForecastDto>>
            GenerateForecastAsync()
        {
            var products =
                await _productRepository.GetAllProductsAsync();

            return products.Select(p =>
                new SalesForecastDto
                {
                    ProductName = p.ProductName,

                    // Placeholder values
                    CurrentMonthSales = 100,
                    PreviousMonthSales = 80,

                    // Simple forecast calculation
                    ForecastedSales = 120
                });
        }
    }
}