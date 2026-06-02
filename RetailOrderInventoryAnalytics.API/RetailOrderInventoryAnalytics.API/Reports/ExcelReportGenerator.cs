using RetailOrderInventoryAnalytics.API.Models.ViewModels;

namespace RetailOrderInventoryAnalytics.API.Reports
{
    public class ExcelReportGenerator
    {
        // US11: Generate Excel Sales Report
        public byte[] GenerateSalesReport(
            SalesReportViewModel report)
        {
            // Placeholder Implementation
            return Array.Empty<byte>();
        }

        // US11: Generate Excel Inventory Report
        public byte[] GenerateInventoryReport(
            IEnumerable<InventoryReportViewModel> report)
        {
            // Placeholder Implementation
            return Array.Empty<byte>();
        }
    }
}