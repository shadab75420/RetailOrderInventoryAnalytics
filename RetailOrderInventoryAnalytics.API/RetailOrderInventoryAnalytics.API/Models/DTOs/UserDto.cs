namespace RetailOrderInventoryAnalytics.API.Models.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}