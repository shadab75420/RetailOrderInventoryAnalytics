using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(
            ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _service.GetAllCategoriesAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result =
                await _service.GetCategoryByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // US2: Add Category
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<IActionResult> Add(
            CategoryDto dto)
        {
            await _service.AddCategoryAsync(dto);

            return Ok();
        }

        // US2: Update Category
        [Authorize(Roles = "Admin,Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CategoryDto dto)
        {
            var result =
                await _service.UpdateCategoryAsync(
                    id,
                    dto);

            if (!result)
                return NotFound();

            return Ok();
        }

        // US2: Delete Category
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var result =
                await _service.DeleteCategoryAsync(id);

            if (!result)
                return NotFound();

            return Ok();
        }
    }
}