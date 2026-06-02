namespace RetailOrderInventoryAnalytics.API.Models.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}