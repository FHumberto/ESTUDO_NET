namespace T_Tier.DAL.Entities;

public class Post : BaseEntity
{
    public int? UserId { get; init; }
    public required string Title { get; init; }
    public required string Body { get; init; }

    //? prop navegação para o usuário que criou o post
    public User? User { get; init; }

    //? prop de navegação para os comentários associados ao post
    public ICollection<Comment>? Comments { get; init; }

    //? prop de navegação para as tags associadas ao post
    public ICollection<Tag>? Tags { get; init; }
}