using RetailOrderInventoryAnalytics.API.Models.ViewModels;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;

        public DashboardService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ISupplierRepository supplierRepository,
            IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
        }

        // US13: Dashboard KPIs
        public async Task<DashboardViewModel>
            GetDashboardDataAsync()
        {
            var products =
                await _productRepository.GetAllProductsAsync();

            var categories =
                await _categoryRepository.GetAllCategoriesAsync();

            var suppliers =
                await _supplierRepository.GetAllSuppliersAsync();

            var orders =
                await _orderRepository.GetAllOrdersAsync();

            return new DashboardViewModel
            {
                TotalProducts = products.Count(),
                TotalCategories = categories.Count(),
                TotalSuppliers = suppliers.Count(),
                TotalOrders = orders.Count(),
                TotalRevenue = orders.Sum(x => x.TotalAmount),
                LowStockProducts = products.Count(
                    x => x.StockQuantity <= x.ReorderLevel)
            };
        }
    }
}