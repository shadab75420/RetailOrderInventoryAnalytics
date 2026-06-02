using RetailOrderInventoryAnalytics.API.Models.ViewModels;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public DashboardService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ISupplierRepository supplierRepository,
            IOrderRepository orderRepository,
            IInventoryRepository inventoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;
            _orderRepository = orderRepository;
            _inventoryRepository = inventoryRepository;
        }

        // US14: Dashboard Enhancement
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

            var transactions =
                await _inventoryRepository.GetAllTransactionsAsync();

            return new DashboardViewModel
            {
                TotalProducts = products.Count(),
                TotalCategories = categories.Count(),
                TotalSuppliers = suppliers.Count(),
                TotalOrders = orders.Count(),
                TotalRevenue = orders.Sum(x => x.TotalAmount),
                LowStockProducts = products.Count(
                    x => x.StockQuantity <= x.ReorderLevel),
                MonthlySalesTrend = orders
                    .GroupBy(x => new
                    {
                        x.OrderDate.Year,
                        x.OrderDate.Month
                    })
                    .OrderBy(x => x.Key.Year)
                    .ThenBy(x => x.Key.Month)
                    .Select(x => new MonthlySalesTrendViewModel
                    {
                        Month = new DateTime(
                            x.Key.Year,
                            x.Key.Month,
                            1).ToString("MMM yyyy"),
                        TotalSales = x.Sum(o => o.TotalAmount)
                    })
                    .ToList(),
                CategoryDistribution = products
                    .GroupBy(x => x.Category?.CategoryName ?? "Unassigned")
                    .Select(x => new CategoryDistributionViewModel
                    {
                        CategoryName = x.Key,
                        ProductCount = x.Count()
                    })
                    .OrderByDescending(x => x.ProductCount)
                    .ToList(),
                RecentTransactions = transactions
                    .OrderByDescending(x => x.TransactionDate)
                    .Take(10)
                    .Select(x => new RecentInventoryTransactionViewModel
                    {
                        ProductName = x.Product?.ProductName ?? string.Empty,
                        TransactionType = x.TransactionType,
                        Quantity = x.Quantity,
                        TransactionDate = x.TransactionDate
                    })
                    .ToList()
            };
        }
    }
}
