using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class ReportsController : Controller
{
    private readonly ReportApiService _reportService;

    public ReportsController(ReportApiService reportService) => _reportService = reportService;

    public async Task<IActionResult> Index()
    {
        if (!CanViewReports()) return RedirectToAction("Index", "Dashboard");

        var model = new ReportsViewModel
        {
            SalesSummary = await _reportService.GetSalesReportAsync() ?? new SalesReportViewModel(),
            InventorySummary = await _reportService.GetInventoryReportAsync()
        };

        return View(model);
    }

    private bool CanViewReports() => new[] { "Admin", "Manager" }.Contains(HttpContext.Session.GetString("Role"));
}
