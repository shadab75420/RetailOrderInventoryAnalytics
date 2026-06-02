using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(
            IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllTransactionsAsync());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProduct(
            int productId)
        {
            return Ok(
                await _service.GetProductTransactionsAsync(
                    productId));
        }

        // US5: Add Inventory Transaction
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Add(
            InventoryTransactionDto dto)
        {
            await _service.AddTransactionAsync(dto);

            return Ok();
        }
    }
}