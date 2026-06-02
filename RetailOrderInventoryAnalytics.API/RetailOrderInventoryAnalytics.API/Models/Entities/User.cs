using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.API.Models.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}