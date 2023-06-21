using System;
using System.Collections.Generic;

namespace MinimalOrdersApi.Data.Entities;

public partial class Staff
{
    public int StaffId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public byte Active { get; set; }

    public int StoreId { get; set; }

    public int? ManagerId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Staff> InverseManager { get; set; } = new List<Staff>();

    public virtual Staff? Manager { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Store Store { get; set; } = null!;
}
