using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(
            IReportService reportService)
        {
            _reportService = reportService;
        }

        // US11: Sales Report
        [HttpGet("sales")]
        public async Task<IActionResult> SalesReport()
        {
            return Ok(
                await _reportService.GetSalesReportAsync());
        }

        // US11: Inventory Report
        [HttpGet("inventory")]
        public async Task<IActionResult> InventoryReport()
        {
            return Ok(
                await _reportService.GetInventoryReportAsync());
        }
    }
}