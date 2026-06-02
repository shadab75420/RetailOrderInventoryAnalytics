namespace RetailOrderInventoryAnalytics.MVC.Models;

public class ForecastViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public DateTime ForecastDate { get; set; }
    public int ForecastQuantity { get; set; }
}
