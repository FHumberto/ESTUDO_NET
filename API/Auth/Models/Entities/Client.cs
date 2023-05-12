using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Entities;

public class Client
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
}
