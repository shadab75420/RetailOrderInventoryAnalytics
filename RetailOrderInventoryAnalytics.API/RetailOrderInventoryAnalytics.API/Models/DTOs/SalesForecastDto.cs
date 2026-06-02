namespace RetailOrderInventoryAnalytics.API.Models.DTOs
{
    public class SalesForecastDto
    {
        public string ProductName { get; set; } = string.Empty;

        public int CurrentMonthSales { get; set; }

        public int PreviousMonthSales { get; set; }

        public double ForecastedSales { get; set; }
    }
}