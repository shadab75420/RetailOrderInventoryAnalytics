using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.API.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}