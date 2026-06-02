using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class DashboardController : Controller
{
    private readonly ReportApiService _reportService;
    private readonly ProductApiService _productService;

    public DashboardController(ReportApiService reportService, ProductApiService productService)
    {
        _reportService = reportService;
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn())
        {
            return RedirectToAction("Login", "Account");
        }

        var model = await _reportService.GetDashboardAsync() ?? new DashboardViewModel();
        ViewBag.LowStockProducts = await _productService.GetLowStockAsync();
        return View(model);
    }

    private bool IsLoggedIn() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("JWToken"));
}
