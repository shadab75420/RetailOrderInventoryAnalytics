using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.MVC.Models;

public class OrderViewModel
{
    public int OrderId { get; set; }

    [Display(Name = "Order Number")]
    public string OrderNumber { get; set; } = string.Empty;

    [Display(Name = "Order Date")]
    public DateTime OrderDate { get; set; } = DateTime.Today;

    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }

    [Required]
    public string Status { get; set; } = "Pending";

    [Display(Name = "User Id")]
    public int UserId { get; set; }
}
