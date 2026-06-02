namespace RetailOrderInventoryAnalytics.API.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task LogActivityAsync(
            string action,
            string entityName,
            string username);

        Task SaveChangesAsync();
    }
}