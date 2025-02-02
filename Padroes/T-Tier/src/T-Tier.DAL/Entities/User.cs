namespace T_Tier.DAL.Entities;

public class User : BaseEntity
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public required string Email { get; set; }
    public required byte[] PasswordHash { get; set; }

    //? prop de navegacao para posts criados pelo usuario
    public ICollection<Post>? Posts { get; set; }

    //? prop de navegacao para comentarios feitos pelo usuario
    public ICollection<Comment>? Comments { get; set; }
}
