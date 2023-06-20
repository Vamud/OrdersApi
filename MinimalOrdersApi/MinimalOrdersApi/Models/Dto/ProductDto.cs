using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int BrandId { get; set; }

        public int CategoryId { get; set; }

        public short ModelYear { get; set; }

        public decimal ListPrice { get; set; }

        public virtual Brand Brand { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
    }
}
