using BCrypt.Net;
using RetailOrderInventoryAnalytics.API.Helpers;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthService(
            IUserRepository userRepository,
            JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        // US1: Register User
        // US1: Register User
        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var existingUser =
                await _userRepository.GetUserByUsernameAsync(dto.Username);

            if (existingUser != null)
            {
                return false;
            }

            // US1: Public registration creates Staff users only
            var user = new User
            {
                FullName = dto.FullName,
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = 3
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        // US1: Login User
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user =
                await _userRepository.GetUserByUsernameAsync(dto.Username);

            if (user == null)
            {
                return null;
            }

            bool validPassword =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!validPassword)
            {
                return null;
            }

            return _jwtHelper.GenerateToken(
                user,
                user.Role!.RoleName);
        }
    }
}