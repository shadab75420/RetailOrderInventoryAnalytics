using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // US13: Dashboard KPIs
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            return Ok(
                await _dashboardService.GetDashboardDataAsync());
        }
    }
}