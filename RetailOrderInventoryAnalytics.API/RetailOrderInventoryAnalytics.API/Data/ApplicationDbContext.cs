using Microsoft.EntityFrameworkCore;
using RetailOrderInventoryAnalytics.API.Entities;
using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // US1: Authentication & Authorization
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        // US2: Category Management
        public DbSet<Category> Categories { get; set; }

        // US3: Supplier Management
        public DbSet<Supplier> Suppliers { get; set; }

        // US4: Product Management
        public DbSet<Product> Products { get; set; }

        // US5: Inventory Management
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        // US6: Order Management
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        // US7: Sales Forecasting
        public DbSet<SalesForecast> SalesForecasts { get; set; }

        // US8: Audit Trail
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // US1: Configure User and Role relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // US2: Configure Product and Category relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // US3: Configure Product and Supplier relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // US4: Configure InventoryTransaction and Product relationship
            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(i => i.Product)
                .WithMany(p => p.InventoryTransactions)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // US6: Configure Order and User relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // US6: Configure OrderItem and Order relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // US6: Configure OrderItem and Product relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // US7: Configure SalesForecast and Product relationship
            modelBuilder.Entity<SalesForecast>()
                .HasOne(sf => sf.Product)
                .WithMany()
                .HasForeignKey(sf => sf.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // US6: Configure decimal precision for Order
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // US6: Configure decimal precision for OrderItem
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // US7: Configure decimal precision for SalesForecast
            modelBuilder.Entity<SalesForecast>()
                .Property(sf => sf.PredictedSales)
                .HasColumnType("decimal(18,2)");
        }
    }
}