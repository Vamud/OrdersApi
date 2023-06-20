using AutoMapper;
using MinimalOrdersApi.Data.Entities;
using MinimalOrdersApi.Models.Dto;

namespace MinimalOrdersApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Store, StoreDto>();
            CreateMap<Stock, StockDto>();
            CreateMap<Brand, BrandDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}
