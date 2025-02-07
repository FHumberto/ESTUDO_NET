namespace T_Tier.DAL.Entities;

public class Post : BaseEntity
{
    public int? UserId { get; set; }
    public required string Tittle { get; set; }
    public required string Body { get; set; }

    //? prop navegação para o usuário que criou o post
    public User? User { get; set; }

    //? prop de navegação para os comentários associados ao post
    public ICollection<Comment>? Comments { get; set; }

    //? prop de navegação para as tags associadas ao post
    public ICollection<Tag>? Tags { get; set; }
}
