using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;
using RetailOrderInventoryAnalytics.MVC.Models;

namespace RetailOrderInventoryAnalytics.MVC.Services;

public class AuthApiService
{
    private readonly ApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthApiService(ApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> LoginAsync(LoginViewModel model)
    {
        var result = await _apiService.PostAsync<LoginViewModel, LoginResponse>("Auth/login", model);
        if (string.IsNullOrWhiteSpace(result?.Token))
        {
            return false;
        }

        var session = _httpContextAccessor.HttpContext?.Session;
        session?.SetString("JWToken", result.Token);

        var principal = ReadToken(result.Token);
        session?.SetString("Username", principal.Username);
        session?.SetString("Role", principal.Role);
        session?.SetString("UserId", principal.UserId);
        return true;
    }

    public Task<bool> RegisterAsync(RegisterViewModel model) => _apiService.PostAsync("Auth/register", model);

    public void Logout() => _httpContextAccessor.HttpContext?.Session.Clear();

    private static (string Username, string Role, string UserId) ReadToken(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var username = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "unique_name" || c.Type == "name")?.Value ?? "User";
        var role = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value ?? string.Empty;
        var userId = jwt.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? string.Empty;
        return (username, role, userId);
    }

    private class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
