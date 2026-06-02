using Microsoft.EntityFrameworkCore;
using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            // US1: Seed default roles
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role
                    {
                        RoleName = "Admin"
                    },
                    new Role
                    {
                        RoleName = "Manager"
                    },
                    new Role
                    {
                        RoleName = "Staff"
                    });

                await context.SaveChangesAsync();
            }

            // US1: Seed default admin user
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    FullName = "System Administrator",
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    RoleId = context.Roles
                        .First(r => r.RoleName == "Admin")
                        .RoleId
                });

                await context.SaveChangesAsync();
            }
        }
    }
}