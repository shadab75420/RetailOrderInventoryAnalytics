namespace RetailOrderInventoryAnalytics.API.Models.DTOs
{
    public class InventoryTransactionDto
    {
        public int TransactionId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string TransactionType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}