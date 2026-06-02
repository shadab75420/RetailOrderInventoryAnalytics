using RetailOrderInventoryAnalytics.API.Data;
using RetailOrderInventoryAnalytics.API.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // US10: Audit logging
        public async Task LogActivityAsync(
            string action,
            string entityName,
            string username)
        {
            var log = new AuditLog
            {
                Action = action,
                EntityName = entityName,
                UserName = username,
                ActionDate = DateTime.UtcNow
            };

            await _context.AuditLogs.AddAsync(log);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}