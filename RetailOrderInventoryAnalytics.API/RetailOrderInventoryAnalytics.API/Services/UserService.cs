using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class UserService : IUserService
    {
        private static readonly string[] ValidRoles =
        {
            "Admin",
            "Manager",
            "Staff"
        };

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // US13: User Management
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users =
                await _userRepository.GetAllUsersAsync();

            return users.Select(x => new UserDto
            {
                UserId = x.UserId,
                FullName = x.FullName,
                Username = x.Username,
                RoleName = x.Role?.RoleName ?? string.Empty
            });
        }

        // US13: User Management
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user =
                await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Username = user.Username,
                RoleName = user.Role?.RoleName ?? string.Empty
            };
        }

        // US13: User Management
        public async Task<bool> UpdateUserRoleAsync(
            int id,
            UpdateUserRoleDto dto,
            int currentUserId)
        {
            if (!ValidRoles.Contains(dto.RoleName))
            {
                return false;
            }

            if (id == currentUserId)
            {
                return false;
            }

            var user =
                await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            if (IsDefaultAdmin(user))
            {
                return false;
            }

            var role =
                await _userRepository.GetRoleByNameAsync(dto.RoleName);

            if (role == null)
            {
                return false;
            }

            user.RoleId = role.RoleId;

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        private static bool IsDefaultAdmin(UserDto user)
        {
            return user.Username.Equals(
                "admin",
                StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsDefaultAdmin(
            RetailOrderInventoryAnalytics.API.Models.Entities.User user)
        {
            return user.Username.Equals(
                "admin",
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
