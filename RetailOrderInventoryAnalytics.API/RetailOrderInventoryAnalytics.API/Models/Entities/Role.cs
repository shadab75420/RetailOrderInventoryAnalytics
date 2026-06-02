using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.API.Models.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; }
            = new List<User>();
    }
}