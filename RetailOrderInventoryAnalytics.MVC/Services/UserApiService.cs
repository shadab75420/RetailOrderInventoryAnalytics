using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class UserApiService
{
    private readonly ApiService _apiService;

    public UserApiService(ApiService apiService)
    {
        _apiService = apiService;
    }

    // US13: User Management
    public async Task<List<UserViewModel>> GetAllAsync()
    {
        return await _apiService.GetAsync<List<UserViewModel>>("User")
            ?? new List<UserViewModel>();
    }

    // US13: User Management
    public Task<UserViewModel?> GetByIdAsync(int id)
    {
        return _apiService.GetAsync<UserViewModel>($"User/{id}");
    }

    // US13: User Management
    public Task<bool> UpdateRoleAsync(UpdateUserRoleViewModel model)
    {
        return _apiService.PutAsync(
            $"User/{model.UserId}/role",
            new
            {
                model.RoleName
            });
    }
}
