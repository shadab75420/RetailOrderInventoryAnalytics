using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class InventoryController : Controller
{
    private readonly InventoryApiService _inventoryService;

    public InventoryController(InventoryApiService inventoryService) => _inventoryService = inventoryService;

    public async Task<IActionResult> Index(int? productId)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

        ViewBag.ProductId = productId;
        var model = productId.HasValue
            ? await _inventoryService.GetByProductAsync(productId.Value)
            : await _inventoryService.GetAllAsync();

        return View(model);
    }

    private bool IsLoggedIn() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("JWToken"));
}
