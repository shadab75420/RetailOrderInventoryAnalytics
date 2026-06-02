using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.API.Models.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(100)]
        public string SupplierName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? ContactPerson { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}