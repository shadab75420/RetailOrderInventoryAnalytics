using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class ProductController : Controller
{
    private readonly ProductApiService _productService;
    private readonly CategoryApiService _categoryService;
    private readonly SupplierApiService _supplierService;

    public ProductController(ProductApiService productService, CategoryApiService categoryService, SupplierApiService supplierService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _supplierService = supplierService;
    }

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(await _productService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var model = await _productService.GetByIdAsync(id);
        return model == null ? NotFound() : View(model);
    }

    public async Task<IActionResult> Create()
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        await PopulateLookups();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        if (!ModelState.IsValid)
        {
            await PopulateLookups();
            return View(model);
        }

        if (await _productService.CreateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to create product.");
        await PopulateLookups();
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        var model = await _productService.GetByIdAsync(id);
        if (model == null) return NotFound();
        await PopulateLookups();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        if (!ModelState.IsValid)
        {
            await PopulateLookups();
            return View(model);
        }

        if (await _productService.UpdateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to update product.");
        await PopulateLookups();
        return View(model);
    }

    private async Task PopulateLookups()
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "CategoryId", "CategoryName");
        ViewBag.Suppliers = new SelectList(await _supplierService.GetAllAsync(), "SupplierId", "SupplierName");
    }

    private bool IsLoggedIn() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("JWToken"));
    private bool CanManage() => IsLoggedIn() && new[] { "Admin", "Manager" }.Contains(HttpContext.Session.GetString("Role"));
}
