using System;
using System.Collections.Generic;

namespace MinimalOrdersApi.Data.Entities;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal ListPrice { get; set; }

    public decimal Discount { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
