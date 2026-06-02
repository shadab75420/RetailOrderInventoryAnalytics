using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetProductByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // US4: Get Low Stock Products
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            return Ok(await _service.GetLowStockProductsAsync());
        }

        // US4: Add Product
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Add(ProductDto dto)
        {
            await _service.AddProductAsync(dto);
            return Ok();
        }

        // US4: Update Product
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            ProductDto dto)
        {
            var result =
                await _service.UpdateProductAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        // US4: Delete Product
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _service.DeleteProductAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}