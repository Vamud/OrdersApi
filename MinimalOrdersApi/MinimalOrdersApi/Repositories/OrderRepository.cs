using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalOrdersApi.Data;
using MinimalOrdersApi.Data.Entities;
using MinimalOrdersApi.Models.Dto;
using MinimalOrdersApi.Models.Enums;
using MinimalOrdersApi.Models.Requests;
using MinimalOrdersApi.Models.Response;
using MinimalOrdersApi.Repositories.Interfases;

namespace MinimalOrdersApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(CreateOrderRequest orderRequest)
        {
            var order = new Order()
            {
                CustomerId = orderRequest.CustomerId,
                OrderStatus = 3,
                OrderDate = DateTime.Now,
                StaffId = orderRequest.StaffId,
                StoreId = orderRequest.StoreId
            };

            var item = await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return item.Entity.OrderId;
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            IQueryable<Order> query = _dbContext.Orders;

            var order = await query.Where(o => o.OrderId == id)
                .Include(c => c.Customer)
                .Include(s => s.Store)
                .Include(s => s.Staff)
                .Include(i => i.OrderItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<PaginatedResponse<OrderDto>> GetByPageAsync(int pageIndex)
        {
            var pageResults = 10f;
            var pageCount = Math.Ceiling(_dbContext.Orders.Count() / pageResults);

            var orders = await _dbContext.Orders
                .Skip((pageIndex - 1) * (int)pageResults)
                .Take((int)pageResults)
                .Include(c => c.Customer)
                .Include(s => s.Store)
                .Include(s => s.Staff)
                .Include(i => i.OrderItems)
                .ThenInclude(p => p.Product)
                .Select(s => _mapper.Map<OrderDto>(s))
                .ToListAsync();

            return new PaginatedResponse<OrderDto>()
            {
                Items = orders,
                Pages = (int)pageCount,
                CurrentPage = pageIndex
            };
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(i => i.OrderId == orderId);

            if (item != null)
            {
                item.OrderStatus = (int)OrderStatus.Canceled;

                _dbContext.SaveChanges();
            }
        }
    }
}
