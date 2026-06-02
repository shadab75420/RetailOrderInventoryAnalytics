using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services;

namespace RetailOrderInventoryAnalytics.Tests.Services
{
    [TestClass]
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> _categoryRepository = null!;
        private Mock<IAuditRepository> _auditRepository = null!;
        private CategoryService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _categoryRepository =
                new Mock<ICategoryRepository>();

            _auditRepository =
                new Mock<IAuditRepository>();

            _service =
                new CategoryService(
                    _categoryRepository.Object,
                    _auditRepository.Object);
        }

        [TestMethod]
        public async Task GetAllCategoriesAsync_ReturnsCategories()
        {
            // US2: Get all categories

            var categories = new List<Category>
            {
                new()
                {
                    CategoryId = 1,
                    CategoryName = "Electronics"
                },
                new()
                {
                    CategoryId = 2,
                    CategoryName = "Clothing"
                }
            };

            _categoryRepository
                .Setup(x => x.GetAllCategoriesAsync())
                .ReturnsAsync(categories);

            var result =
                await _service.GetAllCategoriesAsync();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task AddCategoryAsync_AddsCategorySuccessfully()
        {
            // US2: Add category

            var dto = new CategoryDto
            {
                CategoryName = "Electronics"
            };

            var result =
                await _service.AddCategoryAsync(dto);

            Assert.IsTrue(result);

            _categoryRepository.Verify(
                x => x.AddCategoryAsync(
                    It.IsAny<Category>()),
                Times.Once);

            _auditRepository.Verify(
                x => x.LogActivityAsync(
                    "Create",
                    "Category",
                    "System"),
                Times.Once);
        }

        [TestMethod]
        public async Task DeleteCategoryAsync_RemovesCategorySuccessfully()
        {
            // US2: Delete category

            var category = new Category
            {
                CategoryId = 1,
                CategoryName = "Electronics"
            };

            _categoryRepository
                .Setup(x => x.GetCategoryByIdAsync(1))
                .ReturnsAsync(category);

            var result =
                await _service.DeleteCategoryAsync(1);

            Assert.IsTrue(result);

            _categoryRepository.Verify(
                x => x.DeleteCategory(category),
                Times.Once);

            _auditRepository.Verify(
                x => x.LogActivityAsync(
                    "Delete",
                    "Category",
                    "System"),
                Times.Once);
        }
    }
}