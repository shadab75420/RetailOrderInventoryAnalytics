using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Helpers;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IUserRepository> _userRepository = null!;
        private AuthService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();

            var settings = new Dictionary<string, string?>
            {
                { "Jwt:Key", "ThisIsASuperSecretKeyForUnitTesting12345" },
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" },
                { "Jwt:DurationInMinutes", "60" }
            };

            IConfiguration configuration =
                new ConfigurationBuilder()
                    .AddInMemoryCollection(settings)
                    .Build();

            var jwtHelper = new JwtHelper(configuration);

            _service = new AuthService(
                _userRepository.Object,
                jwtHelper);
        }

        [TestMethod]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // US1: Login with valid credentials

            var password = "Password123";

            var user = new User
            {
                UserId = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = new Role
                {
                    RoleName = "Admin"
                }
            };

            _userRepository
                .Setup(x => x.GetUserByUsernameAsync("admin"))
                .ReturnsAsync(user);

            var dto = new LoginDto
            {
                Username = "admin",
                Password = password
            };

            var result = await _service.LoginAsync(dto);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public async Task Login_WithInvalidCredentials_ReturnsNull()
        {
            // US1: Login with invalid credentials

            _userRepository
                .Setup(x => x.GetUserByUsernameAsync("admin"))
                .ReturnsAsync((User?)null);

            var dto = new LoginDto
            {
                Username = "admin",
                Password = "wrong"
            };

            var result = await _service.LoginAsync(dto);

            Assert.IsNull(result);
        }
    }
}