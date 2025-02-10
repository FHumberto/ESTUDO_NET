namespace T_Tier.DAL.Entities;

public class Comment : BaseEntity
{
    public int? UserId { get; init; }
    public int PostId { get; init; }
    public required string Body { get; init; }

    //? prop de navegação para o usuário que fez o comentário
    public User? User { get; init; }

    //? prop de navegação para o post associado ao comentário
    public Post? Post { get; init; }
}
