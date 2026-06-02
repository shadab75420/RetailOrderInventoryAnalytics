using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;
using RetailOrderInventoryAnalytics.API.Repositories.Interfaces;
using RetailOrderInventoryAnalytics.API.Services.Interfaces;

namespace RetailOrderInventoryAnalytics.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuditRepository _auditRepository;

        public ProductService(
            IProductRepository productRepository,
            IAuditRepository auditRepository)
        {
            _productRepository = productRepository;
            _auditRepository = auditRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.CategoryName ?? "",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.SupplierName ?? "",
                UnitPrice = p.UnitPrice,
                StockQuantity = p.StockQuantity,
                ReorderLevel = p.ReorderLevel
            });
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var p = await _productRepository.GetProductByIdAsync(id);

            if (p == null)
                return null;

            return new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.CategoryName ?? "",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.SupplierName ?? "",
                UnitPrice = p.UnitPrice,
                StockQuantity = p.StockQuantity,
                ReorderLevel = p.ReorderLevel
            };
        }
        // Below method is for US14: Include Category and Supplier Info in Low Stock Products
        public async Task<IEnumerable<ProductDto>> GetLowStockProductsAsync()
        {
            var products = await _productRepository.GetLowStockProductsAsync();

            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,

                CategoryId = p.CategoryId,

                // US14: Include Category Name in Low Stock Products
                CategoryName = p.Category?.CategoryName ?? "",

                SupplierId = p.SupplierId,

                // US14: Include Supplier Name in Low Stock Products
                SupplierName = p.Supplier?.SupplierName ?? "",

                UnitPrice = p.UnitPrice,
                StockQuantity = p.StockQuantity,
                ReorderLevel = p.ReorderLevel
            });
        }

        // US4: Add Product
        public async Task<bool> AddProductAsync(ProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId,
                UnitPrice = dto.UnitPrice,
                StockQuantity = dto.StockQuantity,
                ReorderLevel = dto.ReorderLevel
            };

            await _productRepository.AddProductAsync(product);
            await _productRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Create",
                "Product",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US4: Update Product
        public async Task<bool> UpdateProductAsync(int id, ProductDto dto)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return false;

            product.ProductName = dto.ProductName;
            product.CategoryId = dto.CategoryId;
            product.SupplierId = dto.SupplierId;
            product.UnitPrice = dto.UnitPrice;
            product.StockQuantity = dto.StockQuantity;
            product.ReorderLevel = dto.ReorderLevel;

            _productRepository.UpdateProduct(product);
            await _productRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Update",
                "Product",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }

        // US4: Delete Product
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
                return false;

            _productRepository.DeleteProduct(product);
            await _productRepository.SaveChangesAsync();

            // US8: Audit Logging
            await _auditRepository.LogActivityAsync(
                "Delete",
                "Product",
                "System");

            await _auditRepository.SaveChangesAsync();

            return true;
        }
    }
}