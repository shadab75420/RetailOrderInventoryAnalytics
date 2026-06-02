using RetailOrderInventoryAnalytics.API.Models.DTOs;
namespace RetailOrderInventoryAnalytics.API.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }

        public int TotalCategories { get; set; }

        public int TotalSuppliers { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public int LowStockProducts { get; set; }

        public List<MonthlySalesTrendViewModel> MonthlySalesTrend { get; set; }
            = new();

        public List<CategoryDistributionViewModel> CategoryDistribution { get; set; }
            = new();

        public List<RecentInventoryTransactionViewModel> RecentTransactions { get; set; }
            = new();
    }

    public class MonthlySalesTrendViewModel
    {
        public string Month { get; set; } = string.Empty;

        public decimal TotalSales { get; set; }
    }

    public class CategoryDistributionViewModel
    {
        public string CategoryName { get; set; } = string.Empty;

        public int ProductCount { get; set; }
    }

    public class RecentInventoryTransactionViewModel
    {
        public string ProductName { get; set; } = string.Empty;

        public string TransactionType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
