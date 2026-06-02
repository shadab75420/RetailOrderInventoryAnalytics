using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.MVC.Models;

public class CategoryViewModel
{
    public int CategoryId { get; set; }

    [Required]
    [Display(Name = "Category Name")]
    public string CategoryName { get; set; } = string.Empty;
}
