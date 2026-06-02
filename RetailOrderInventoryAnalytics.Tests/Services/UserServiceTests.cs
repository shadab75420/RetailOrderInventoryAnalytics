using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepository = null!;
        private UserService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _userRepository =
                new Mock<IUserRepository>();

            _service =
                new UserService(
                    _userRepository.Object);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_ReturnsUsersWithRoles()
        {
            // US13: User Management

            var users = new List<User>
            {
                new()
                {
                    UserId = 1,
                    FullName = "System Administrator",
                    Username = "admin",
                    Role = new Role
                    {
                        RoleName = "Admin"
                    }
                },
                new()
                {
                    UserId = 2,
                    FullName = "Store Staff",
                    Username = "staff",
                    Role = new Role
                    {
                        RoleName = "Staff"
                    }
                }
            };

            _userRepository
                .Setup(x => x.GetAllUsersAsync())
                .ReturnsAsync(users);

            var result =
                (await _service.GetAllUsersAsync()).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Admin", result[0].RoleName);
            Assert.AreEqual("Staff", result[1].RoleName);
        }

        [TestMethod]
        public async Task UpdateUserRoleAsync_WithValidRole_UpdatesRole()
        {
            // US13: User Management

            var user = new User
            {
                UserId = 2,
                Username = "staff",
                RoleId = 3,
                Role = new Role
                {
                    RoleName = "Staff"
                }
            };

            var managerRole = new Role
            {
                RoleId = 2,
                RoleName = "Manager"
            };

            _userRepository
                .Setup(x => x.GetUserByIdAsync(2))
                .ReturnsAsync(user);

            _userRepository
                .Setup(x => x.GetRoleByNameAsync("Manager"))
                .ReturnsAsync(managerRole);

            var result =
                await _service.UpdateUserRoleAsync(
                    2,
                    new UpdateUserRoleDto
                    {
                        RoleName = "Manager"
                    },
                    1);

            Assert.IsTrue(result);
            Assert.AreEqual(2, user.RoleId);

            _userRepository.Verify(
                x => x.UpdateUser(user),
                Times.Once);

            _userRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [TestMethod]
        public async Task UpdateUserRoleAsync_WhenChangingOwnRole_ReturnsFalse()
        {
            // US13: User Management

            var result =
                await _service.UpdateUserRoleAsync(
                    1,
                    new UpdateUserRoleDto
                    {
                        RoleName = "Manager"
                    },
                    1);

            Assert.IsFalse(result);
            _userRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Never);
        }

        [TestMethod]
        public async Task UpdateUserRoleAsync_WhenDefaultAdmin_ReturnsFalse()
        {
            // US13: User Management

            var user = new User
            {
                UserId = 1,
                Username = "admin",
                RoleId = 1
            };

            _userRepository
                .Setup(x => x.GetUserByIdAsync(1))
                .ReturnsAsync(user);

            var result =
                await _service.UpdateUserRoleAsync(
                    1,
                    new UpdateUserRoleDto
                    {
                        RoleName = "Manager"
                    },
                    99);

            Assert.IsFalse(result);
            _userRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Never);
        }

        [TestMethod]
        public async Task UpdateUserRoleAsync_WithInvalidRole_ReturnsFalse()
        {
            // US13: User Management

            var result =
                await _service.UpdateUserRoleAsync(
                    2,
                    new UpdateUserRoleDto
                    {
                        RoleName = "Supervisor"
                    },
                    1);

            Assert.IsFalse(result);
            _userRepository.Verify(
                x => x.SaveChangesAsync(),
                Times.Never);
        }
    }
}
