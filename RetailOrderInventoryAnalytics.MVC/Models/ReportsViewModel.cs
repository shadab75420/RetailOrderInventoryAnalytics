namespace RetailOrderInventoryAnalytics.MVC.Models;

public class ReportsViewModel
{
    public SalesReportViewModel SalesSummary { get; set; } = new();
    public List<InventoryReportViewModel> InventorySummary { get; set; } = new();
}

public class SalesReportViewModel
{
    public DateTime ReportDate { get; set; }
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public decimal AverageOrderValue { get; set; }
}

public class InventoryReportViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int ReorderLevel { get; set; }
    public bool IsLowStock => StockQuantity <= ReorderLevel;
}
