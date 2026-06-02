namespace RetailOrderInventoryAnalytics.API.Entities;

public class AuditLog
{
    public int Id { get; set; }

    public string Action { get; set; } = string.Empty;

    public string EntityName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public DateTime ActionDate { get; set; } = DateTime.UtcNow;

    public string Details { get; set; } = string.Empty;
}