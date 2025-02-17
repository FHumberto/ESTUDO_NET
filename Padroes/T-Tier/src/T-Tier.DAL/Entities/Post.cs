using T_Tier.DAL.Contracts;

namespace T_Tier.DAL.Entities;

public class Post : BaseEntity, ISoftDeleteEntity
{
    public string? UserId { get; set; }
    public required string Title { get; init; }
    public required string Body { get; init; }
    public bool IsDeleted { get; private set; } = false;

    //? prop navegação para o usuário que criou o post
    public User? User { get; init; }

    //? prop de navegação para os comentários associados ao post
    public ICollection<Comment>? Comments { get; init; }

    //? prop de navegação para as tags associadas ao post
    public ICollection<Tag>? Tags { get; init; }

    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}