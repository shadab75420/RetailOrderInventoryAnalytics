namespace RetailOrderInventoryAnalytics.API.Models.ViewModels
{
    public class InventoryReportViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public int ReorderLevel { get; set; }

        public bool IsLowStock => StockQuantity <= ReorderLevel;
    }
}