using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Domain.Models;

public class Usuario : Notifiable<Notification>
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    [Required, EmailAddress]
    public string? Email { get; set; }

    public Usuario(string? nome, string? sobrenome, string? email)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;

        Validar();
    }

    public void Editar(string? nome, string? sobrenome, string? email)
    {
        Nome = nome;
        Sobrenome = sobrenome;
        Email = email;

        Validar();
    }


    private void Validar()
    {
        var contract = new Contract<Usuario>()
            .IsNotNullOrEmpty(Nome, "Nome")
            .IsGreaterOrEqualsThan(Nome, 3, "Nome")
            .IsEmailOrEmpty(Email, "Email");
        AddNotifications(contract);
    }
}
