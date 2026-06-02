using AutoMapper;
using RetailOrderInventoryAnalytics.API.Models.DTOs;
using RetailOrderInventoryAnalytics.API.Models.Entities;

namespace RetailOrderInventoryAnalytics.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // US1: User Mapping
            CreateMap<User, UserDto>().ReverseMap();

            // US2: Category Mapping
            CreateMap<Category, CategoryDto>().ReverseMap();

            // US3: Supplier Mapping
            CreateMap<Supplier, SupplierDto>().ReverseMap();

            // US4: Product Mapping
            CreateMap<Product, ProductDto>().ReverseMap();

            // US5: Inventory Mapping
            CreateMap<InventoryTransaction, InventoryTransactionDto>()
                .ReverseMap();

            // US6: Order Mapping
            CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}