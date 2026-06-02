using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto?> GetUserByIdAsync(int id);

        Task<bool> UpdateUserRoleAsync(
            int id,
            UpdateUserRoleDto dto,
            int currentUserId);
    }
}
