using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class SupplierController : Controller
{
    private readonly SupplierApiService _supplierService;

    public SupplierController(SupplierApiService supplierService) => _supplierService = supplierService;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(await _supplierService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var model = await _supplierService.GetByIdAsync(id);
        return model == null ? NotFound() : View(model);
    }

    public IActionResult Create()
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SupplierViewModel model)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        if (!ModelState.IsValid) return View(model);

        if (await _supplierService.CreateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to create supplier.");
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        var model = await _supplierService.GetByIdAsync(id);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SupplierViewModel model)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        if (!ModelState.IsValid) return View(model);

        if (await _supplierService.UpdateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to update supplier.");
        return View(model);
    }

    private bool IsLoggedIn() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("JWToken"));
    private bool CanManage() => IsLoggedIn() && new[] { "Admin", "Manager" }.Contains(HttpContext.Session.GetString("Role"));
}
