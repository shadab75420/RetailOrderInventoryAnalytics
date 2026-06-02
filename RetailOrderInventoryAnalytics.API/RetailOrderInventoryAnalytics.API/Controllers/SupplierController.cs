using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(
            ISupplierService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllSuppliersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result =
                await _service.GetSupplierByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // US3: Add Supplier
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Add(
            SupplierDto dto)
        {
            await _service.AddSupplierAsync(dto);

            return Ok();
        }

        // US3: Update Supplier
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            SupplierDto dto)
        {
            var result =
                await _service.UpdateSupplierAsync(
                    id,
                    dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        // US3: Delete Supplier
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var result =
                await _service.DeleteSupplierAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}