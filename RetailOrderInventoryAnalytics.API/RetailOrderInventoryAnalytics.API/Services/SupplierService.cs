using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAuditRepository _auditRepository;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IAuditRepository auditRepository)
        {
            _supplierRepository = supplierRepository;
            _auditRepository = auditRepository;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllSuppliersAsync()
        {
            var suppliers =
                await _supplierRepository.GetAllSuppliersAsync();

            return suppliers.Select(s => new SupplierDto
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                ContactPerson = s.ContactPerson,
                PhoneNumber = s.PhoneNumber,
                Email = s.Email
            });
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            var supplier =
                await _supplierRepository.GetSupplierByIdAsync(id);

            if (supplier == null)
                return null;

            return new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                ContactPerson = supplier.ContactPerson,
                PhoneNumber = supplier.PhoneNumber,
                Email = supplier.Email
            };
        }

        // US3: Add Supplier
        public async Task<bool> AddSupplierAsync(SupplierDto dto)
        {
            var supplier = new Supplier
            {
                SupplierName = dto.SupplierName,
                ContactPerson = dto.ContactPerson,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email
            };

            await _supplierRepository.AddSupplierAsync(supplier);
            await _supplierRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Create",
                "Supplier",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US3: Update Supplier
        public async Task<bool> UpdateSupplierAsync(int id, SupplierDto dto)
        {
            var supplier =
                await _supplierRepository.GetSupplierByIdAsync(id);

            if (supplier == null)
                return false;

            supplier.SupplierName = dto.SupplierName;
            supplier.ContactPerson = dto.ContactPerson;
            supplier.PhoneNumber = dto.PhoneNumber;
            supplier.Email = dto.Email;

            _supplierRepository.UpdateSupplier(supplier);
            await _supplierRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Update",
                "Supplier",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US3: Delete Supplier
        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier =
                await _supplierRepository.GetSupplierByIdAsync(id);

            if (supplier == null)
                return false;

            _supplierRepository.DeleteSupplier(supplier);
            await _supplierRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Delete",
                "Supplier",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }
    }
}