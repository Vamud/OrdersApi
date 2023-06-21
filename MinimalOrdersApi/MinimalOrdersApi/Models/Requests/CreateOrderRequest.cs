using MinimalOrdersApi.Models.Dto;

namespace MinimalOrdersApi.Models.Requests
{
    public class CreateOrderRequest
    {
        public int? CustomerId { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int StoreId { get; set; }

        public int StaffId { get; set; }
    }
}
