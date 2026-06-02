using Microsoft.AspNetCore.Mvc;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            _authService = authService;
        }

        // US1: User Registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterDto dto)
        {
            var result =
                await _authService.RegisterAsync(dto);

            if (!result)
            {
                return BadRequest(
                    "Username already exists.");
            }

            return Ok(
                "User registered successfully.");
        }

        // US1: User Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginDto dto)
        {
            var token =
                await _authService.LoginAsync(dto);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(
                    "Invalid username or password.");
            }

            return Ok(new
            {
                Token = token
            });
        }
    }
}