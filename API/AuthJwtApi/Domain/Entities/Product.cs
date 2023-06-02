namespace AuthJwtApi.Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public float Price { get; set; }
    public string? Description { get; set; } = string.Empty;
}
