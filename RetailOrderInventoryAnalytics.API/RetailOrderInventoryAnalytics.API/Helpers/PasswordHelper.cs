using BCrypt.Net;

namespace RetailOrderInventoryAnalytics.API.Helpers
{
    public static class PasswordHelper
    {
        // US1: Hash Password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // US1: Verify Password
        public static bool VerifyPassword(
            string password,
            string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(
                password,
                passwordHash);
        }
    }
}