using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Dto
{
    public class BrandDto
    {
        public int BrandId { get; set; }

        public string BrandName { get; set; } = null!;

        public virtual ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
