using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class OrderController : Controller
{
    private readonly OrderApiService _orderService;

    public OrderController(OrderApiService orderService) => _orderService = orderService;

    public async Task<IActionResult> Index()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(await _orderService.GetAllAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        var model = await _orderService.GetByIdAsync(id);
        return model == null ? NotFound() : View(model);
    }

    public IActionResult Create()
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        return View(new OrderViewModel { OrderDate = DateTime.Today, Status = "Pending" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(OrderViewModel model)
    {
        if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
        if (!ModelState.IsValid) return View(model);

        if (await _orderService.CreateAsync(model)) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Unable to create order.");
        return View(model);
    }

    private bool IsLoggedIn() => !string.IsNullOrWhiteSpace(HttpContext.Session.GetString("JWToken"));
}
