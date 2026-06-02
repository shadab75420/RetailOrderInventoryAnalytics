using RetailOrderInventoryAnalytics.API.Models.DTOs;

namespace RetailOrderInventoryAnalytics.API.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync();

        Task<SupplierDto?> GetSupplierByIdAsync(int id);

        Task<bool> AddSupplierAsync(SupplierDto dto);

        Task<bool> UpdateSupplierAsync(int id, SupplierDto dto);

        Task<bool> DeleteSupplierAsync(int id);
    }
}