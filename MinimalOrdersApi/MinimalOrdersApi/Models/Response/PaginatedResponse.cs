using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Response
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Items { get; init; } = null!;
        public int Pages { get; init; }
        public int CurrentPage { get; init; }
    }
}
