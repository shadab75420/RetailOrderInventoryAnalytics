using System.ComponentModel.DataAnnotations;

namespace RetailOrderInventoryAnalytics.MVC.Models;

public class ProductViewModel
{
    public int ProductId { get; set; }

    [Required]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    [Display(Name = "Category")]
    public string CategoryName { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    [Display(Name = "Supplier")]
    public int SupplierId { get; set; }

    [Display(Name = "Supplier")]
    public string SupplierName { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    [Display(Name = "Unit Price")]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Reorder Level")]
    public int ReorderLevel { get; set; }

    public bool IsLowStock => StockQuantity <= ReorderLevel;
}
