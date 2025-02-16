using T_Tier.DAL.Contracts;

namespace T_Tier.DAL.Entities;

public class Comment : BaseEntity, ISoftDeleteEntity
{
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength
    public string? UserId { get; set; }
    public int PostId { get; init; }
    public required string Body { get; init; }
    public bool IsDeleted { get; private set; } = false;

    //? prop de navegação para o usuário que fez o comentário
    public User? User { get; init; }

    //? prop de navegação para o post associado ao comentário
    public Post? Post { get; init; }

    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}
