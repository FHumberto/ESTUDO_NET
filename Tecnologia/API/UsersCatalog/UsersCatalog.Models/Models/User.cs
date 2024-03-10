using System.ComponentModel.DataAnnotations;

namespace UsersCatalog.Models.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
}
