namespace T_Tier.DAL.Entities;

public class User : BaseEntity
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public required string Email { get; init; }
    public required byte[] PasswordHash { get; init; }

    //? prop de navegacao para posts criados pelo usuario
    public ICollection<Post>? Posts { get; init; }

    //? prop de navegacao para comentarios feitos pelo usuario
    public ICollection<Comment>? Comments { get; init; }
}
