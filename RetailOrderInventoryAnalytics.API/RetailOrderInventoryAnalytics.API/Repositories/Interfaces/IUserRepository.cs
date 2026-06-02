using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User?> GetUserByIdAsync(int id);

        Task<User?> GetUserByUsernameAsync(string username);

        Task<Role?> GetRoleByNameAsync(string roleName);

        Task AddUserAsync(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);

        Task SaveChangesAsync();
    }
}
