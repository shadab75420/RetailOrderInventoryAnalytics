using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Controllers;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.Tests.Controllers
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserService> _userService = null!;
        private UserController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _userService =
                new Mock<IUserService>();

            _controller =
                new UserController(
                    _userService.Object);

            _controller.ControllerContext =
                new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(
                            new ClaimsIdentity(
                                new[]
                                {
                                    new Claim("UserId", "1"),
                                    new Claim(ClaimTypes.Role, "Admin")
                                },
                                "TestAuth"))
                    }
                };
        }

        [TestMethod]
        public async Task GetAll_ReturnsOkWithUsers()
        {
            // US13: User Management

            var users = new List<UserDto>
            {
                new()
                {
                    UserId = 1,
                    FullName = "System Administrator",
                    Username = "admin",
                    RoleName = "Admin"
                }
            };

            _userService
                .Setup(x => x.GetAllUsersAsync())
                .ReturnsAsync(users);

            var result =
                await _controller.GetAll();

            var okResult =
                result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(users, okResult.Value);
        }

        [TestMethod]
        public async Task Get_WhenUserExists_ReturnsOk()
        {
            // US13: User Management

            var user = new UserDto
            {
                UserId = 2,
                FullName = "Store Manager",
                Username = "manager",
                RoleName = "Manager"
            };

            _userService
                .Setup(x => x.GetUserByIdAsync(2))
                .ReturnsAsync(user);

            var result =
                await _controller.Get(2);

            var okResult =
                result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(user, okResult.Value);
        }

        [TestMethod]
        public async Task Get_WhenUserMissing_ReturnsNotFound()
        {
            // US13: User Management

            _userService
                .Setup(x => x.GetUserByIdAsync(99))
                .ReturnsAsync((UserDto?)null);

            var result =
                await _controller.Get(99);

            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public async Task UpdateRole_WithValidRequest_ReturnsOk()
        {
            // US13: User Management

            var dto =
                new UpdateUserRoleDto
                {
                    RoleName = "Manager"
                };

            _userService
                .Setup(x => x.UpdateUserRoleAsync(2, dto, 1))
                .ReturnsAsync(true);

            var result =
                await _controller.UpdateRole(2, dto);

            Assert.IsInstanceOfType<OkResult>(result);
        }

        [TestMethod]
        public async Task UpdateRole_WithInvalidRequest_ReturnsBadRequest()
        {
            // US13: User Management

            var dto =
                new UpdateUserRoleDto
                {
                    RoleName = "Admin"
                };

            _userService
                .Setup(x => x.UpdateUserRoleAsync(2, dto, 1))
                .ReturnsAsync(false);

            var result =
                await _controller.UpdateRole(2, dto);

            Assert.IsInstanceOfType<BadRequestObjectResult>(result);
        }
    }
}
