using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.MVC.Models;

public class SupplierViewModel
{
    public int SupplierId { get; set; }

    [Required]
    [Display(Name = "Supplier Name")]
    public string SupplierName { get; set; } = string.Empty;

    [Display(Name = "Contact Person")]
    public string? ContactPerson { get; set; }

    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}
