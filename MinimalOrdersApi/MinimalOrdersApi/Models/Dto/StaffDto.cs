using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Dto
{
    public class StaffDto
    {
        public int StaffId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public byte Active { get; set; }

        public int? ManagerId { get; set; }

        public virtual Staff? Manager { get; set; }
    }
}
