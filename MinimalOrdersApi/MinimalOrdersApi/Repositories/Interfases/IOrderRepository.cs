using MinimalOrdersApi.Data.Entities;
using MinimalOrdersApi.Models.Dto;
using MinimalOrdersApi.Models.Requests;
using MinimalOrdersApi.Models.Response;

namespace MinimalOrdersApi.Repositories.Interfases
{
    public interface IOrderRepository
    {
        Task<OrderDto?> GetByIdAsync(int id);
        Task<PaginatedResponse<OrderDto>> GetByPageAsync(int pageIndex);
        Task<int> AddAsync(CreateOrderRequest request);
        Task CancelOrderAsync(int orderId);
    }
}
