using System.ComponentModel.DataAnnotations;

namespace Auth.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string? NickName { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? Role { get; set; }
}
