using System.ComponentModel.DataAnnotations;

namespace ProductHub_Domain.Models;
public class Product
{
    [Key]
    public int Product_Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public decimal Price { get; set; }
}
