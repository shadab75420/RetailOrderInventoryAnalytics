using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.MVC.Models;

public class UpdateUserRoleViewModel
{
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string CurrentRoleName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "New Role")]
    public string RoleName { get; set; } = string.Empty;
}
