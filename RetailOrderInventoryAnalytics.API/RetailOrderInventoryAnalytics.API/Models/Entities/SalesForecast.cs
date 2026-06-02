using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Entities;

public class SalesForecast
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public DateTime ForecastDate { get; set; }

    public decimal PredictedSales { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Product? Product { get; set; }
}