using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;
using System.Security.Claims;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // US13: User Management
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _userService.GetAllUsersAsync());
        }

        // US13: User Management
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result =
                await _userService.GetUserByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // US13: User Management
        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateRole(
            int id,
            UpdateUserRoleDto dto)
        {
            var currentUserId =
                GetCurrentUserId();

            var result =
                await _userService.UpdateUserRoleAsync(
                    id,
                    dto,
                    currentUserId);

            if (!result)
            {
                return BadRequest(
                    "Invalid role change request.");
            }

            return Ok();
        }

        private int GetCurrentUserId()
        {
            var userId =
                User.Claims.FirstOrDefault(
                    x => x.Type == "UserId")?.Value;

            return int.TryParse(userId, out var id)
                ? id
                : 0;
        }
    }
}
