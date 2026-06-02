using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.MVC.Models;
using RetailOrderInventoryAnalytics.MVC.Services;

namespace RetailOrderInventoryAnalytics.MVC.Controllers;

public class UserController : Controller
{
    private readonly UserApiService _userService;

    public UserController(UserApiService userService)
    {
        _userService = userService;
    }

    // US13: User Management
    public async Task<IActionResult> Index()
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index", "Dashboard");
        }

        return View(await _userService.GetAllAsync());
    }

    // US13: User Management
    public async Task<IActionResult> EditRole(int id)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index", "Dashboard");
        }

        var user =
            await _userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return View(new UpdateUserRoleViewModel
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Username = user.Username,
            CurrentRoleName = user.RoleName,
            RoleName = user.RoleName
        });
    }

    // US13: User Management
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRole(UpdateUserRoleViewModel model)
    {
        if (!IsAdmin())
        {
            return RedirectToAction("Index", "Dashboard");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _userService.UpdateRoleAsync(model))
        {
            TempData["Success"] = "User role updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(
            string.Empty,
            "Unable to update role. Check role rules and try again.");

        return View(model);
    }

    private bool IsAdmin()
    {
        return HttpContext.Session.GetString("Role") == "Admin";
    }
}
