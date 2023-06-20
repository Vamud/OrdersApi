﻿using MinimalOrdersApi.Data.Entities;

namespace MinimalOrdersApi.Models.Dto
{
    public class StoreDto
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }
    }
}
