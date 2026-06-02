using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();

        Task<Supplier?> GetSupplierByIdAsync(int id);

        Task AddSupplierAsync(Supplier supplier);

        void UpdateSupplier(Supplier supplier);

        void DeleteSupplier(Supplier supplier);

        Task SaveChangesAsync();
    }
}