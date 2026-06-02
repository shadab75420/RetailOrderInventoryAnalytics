using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.API.Models.DTOs
{
    public class UpdateUserRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
