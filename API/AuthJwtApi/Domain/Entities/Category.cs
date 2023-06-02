﻿namespace AuthJwtApi.Domain.Entities;

public class Category : BaseEntity
{
    public string? Name { get; set; }
    public ICollection<Product> Product { get; set; }
}
