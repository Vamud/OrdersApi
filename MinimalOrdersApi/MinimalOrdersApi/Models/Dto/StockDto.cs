using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Dto
{
    public class StockDto
    {
        public int StoreId { get; set; }

        public int ProductId { get; set; }

        public int? Quantity { get; set; }

        public ProductDto Product { get; set; } = null!;

        public StoreDto Store { get; set; } = null!;
    }
}
