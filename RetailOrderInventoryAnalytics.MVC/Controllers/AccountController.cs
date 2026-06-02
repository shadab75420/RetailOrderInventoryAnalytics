using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class AccountController : Controller
{
    private readonly AuthApiService _authService;

    public AccountController(AuthApiService authService) => _authService = authService;

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _authService.LoginAsync(model))
        {
            return RedirectToAction("Index", "Dashboard");
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _authService.RegisterAsync(model))
        {
            TempData["Success"] = "Registration successful. Please sign in.";
            return RedirectToAction(nameof(Login));
        }

        ModelState.AddModelError(string.Empty, "Registration failed. Username may already exist.");
        return View(model);
    }

    public IActionResult Logout()
    {
        _authService.Logout();
        return RedirectToAction(nameof(Login));
    }
}
