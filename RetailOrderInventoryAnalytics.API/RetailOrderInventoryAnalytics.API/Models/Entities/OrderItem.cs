using DocumentFormat.OpenXml.Drawing.Charts;
using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Models.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public RetailOrderInventoryAnalytics.API.Models.Entities.Order? Order { get; set; }

    public Product? Product { get; set; }
}