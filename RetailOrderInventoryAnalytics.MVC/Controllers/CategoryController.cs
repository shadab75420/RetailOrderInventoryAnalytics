using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class CategoryController : Controller
{
    private readonly CategoryApiService _categoryService;

    public CategoryController(CategoryApiService categoryService) => _categoryService = categoryService;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(await _categoryService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var model = await _categoryService.GetByIdAsync(id);
        return model == null ? NotFound() : View(model);
    }

    public IActionResult Create()
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryViewModel model)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        if (!ModelState.IsValid) return View(model);

        if (await _categoryService.CreateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to create category.");
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        var model = await _categoryService.GetByIdAsync(id);
        return model == null ? NotFound() : View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryViewModel model)
    {
        if (!CanManage()) return RedirectToAction(nameof(Index));
        if (!ModelState.IsValid) return View(model);

        if (await _categoryService.UpdateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to update category.");
        return View(model);
    }

    private bool IsLoggedIn() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("JWToken"));
    private bool CanManage() => IsLoggedIn() && new[] { "Admin", "Manager" }.Contains(HttpContext.Session.GetString("Role"));
}
