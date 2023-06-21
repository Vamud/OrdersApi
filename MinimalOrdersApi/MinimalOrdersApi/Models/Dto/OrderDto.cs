using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int? CustomerId { get; set; }

        public byte OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int StoreId { get; set; }

        public int StaffId { get; set; }

        public CustomerDto? Customer { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public StaffDto Staff { get; set; } = null!;

        public StoreDto Store { get; set; } = null!;
    }
}
