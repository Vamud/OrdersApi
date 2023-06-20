using MinimalOrdersApi.Data.Entities;
using MinimalOrdersApi.Models.Dto;
using MinimalOrdersApi.Models.Response;

namespace MinimalOrdersApi.Repositories.Interfases
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<PaginatedResponse<OrderDto>> GetByPageAsync(int pageIndex);
    }
}
