using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthJwtApi.Domain.Entities;

public class Product : BaseEntity
{
    [Required]
    public string? Name { get; set; }
    public float Price { get; set; }
    public string? Description { get; set; } = string.Empty;
    public Category Category { get; set; }
}
