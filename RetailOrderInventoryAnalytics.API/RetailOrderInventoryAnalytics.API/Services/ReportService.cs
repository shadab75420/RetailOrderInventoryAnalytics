using RetailOrderInventoryAnalytics.API.Models.ViewModels;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class ReportService : IReportService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public ReportService(
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        // US11: Generate Sales Report
        public async Task<SalesReportViewModel> GetSalesReportAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            var totalSales = orders.Sum(x => x.TotalAmount);

            var totalOrders = orders.Count();

            return new SalesReportViewModel
            {
                ReportDate = DateTime.UtcNow,
                TotalSales = totalSales,
                TotalOrders = totalOrders,
                AverageOrderValue =
                    totalOrders == 0
                    ? 0
                    : totalSales / totalOrders
            };
        }

        // US11: Generate Inventory Report
        public async Task<IEnumerable<InventoryReportViewModel>>
            GetInventoryReportAsync()
        {
            var products =
                await _productRepository.GetAllProductsAsync();

            return products.Select(p =>
                new InventoryReportViewModel
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    StockQuantity = p.StockQuantity,
                    ReorderLevel = p.ReorderLevel
                });
        }
    }
}