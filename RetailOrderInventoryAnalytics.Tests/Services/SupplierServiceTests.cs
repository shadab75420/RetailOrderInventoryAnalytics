using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class SupplierServiceTests
    {
        private Mock<ISupplierRepository> _supplierRepository = null!;
        private Mock<IAuditRepository> _auditRepository = null!;
        private SupplierService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _supplierRepository = new Mock<ISupplierRepository>();
            _auditRepository = new Mock<IAuditRepository>();

            _service = new SupplierService(
                _supplierRepository.Object,
                _auditRepository.Object);
        }

        [TestMethod]
        public async Task AddSupplierAsync_AddsSupplierSuccessfully()
        {
            // US3: Add Supplier

            var dto = new SupplierDto
            {
                SupplierName = "ABC Supplier",
                ContactPerson = "John",
                PhoneNumber = "9999999999",
                Email = "abc@test.com"
            };

            var result = await _service.AddSupplierAsync(dto);

            Assert.IsTrue(result);

            _supplierRepository.Verify(
                x => x.AddSupplierAsync(It.IsAny<Supplier>()),
                Times.Once);

            _auditRepository.Verify(
                x => x.LogActivityAsync("Create", "Supplier", "System"),
                Times.Once);
        }

        [TestMethod]
        public async Task GetAllSuppliersAsync_ReturnsSuppliers()
        {
            // US3: Get Suppliers

            var suppliers = new List<Supplier>
            {
                new()
                {
                    SupplierId = 1,
                    SupplierName = "ABC"
                }
            };

            _supplierRepository
                .Setup(x => x.GetAllSuppliersAsync())
                .ReturnsAsync(suppliers);

            var result = await _service.GetAllSuppliersAsync();

            Assert.AreEqual(1, result.Count());
        }
    }
}