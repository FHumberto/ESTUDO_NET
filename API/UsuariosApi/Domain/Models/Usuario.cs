using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Domain.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }
}
