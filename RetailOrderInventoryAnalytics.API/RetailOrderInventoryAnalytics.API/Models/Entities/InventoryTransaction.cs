using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.API.Models.Entities
{
    public class InventoryTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(20)]
        public string TransactionType { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }
            = DateTime.UtcNow;

        public Product? Product { get; set; }
    }
}