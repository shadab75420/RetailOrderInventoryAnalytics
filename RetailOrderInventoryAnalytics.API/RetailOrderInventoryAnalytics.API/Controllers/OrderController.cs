using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(
            IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result =
                await _service.GetOrderByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // US6: Create Order
        [HttpPost]
        public async Task<IActionResult> Create(
            OrderDto dto)
        {
            await _service.CreateOrderAsync(dto);

            return Ok();
        }

        // US6: Delete Order
        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var result =
                await _service.DeleteOrderAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}