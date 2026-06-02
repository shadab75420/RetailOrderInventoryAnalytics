namespace RetailOrderInventoryAnalytics.API.Models.ViewModels
{
    public class SalesReportViewModel
    {
        public DateTime ReportDate { get; set; }

        public decimal TotalSales { get; set; }

        public int TotalOrders { get; set; }

        public decimal AverageOrderValue { get; set; }
    }
}