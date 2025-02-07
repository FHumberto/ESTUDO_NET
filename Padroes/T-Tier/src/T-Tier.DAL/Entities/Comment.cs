namespace T_Tier.DAL.Entities;

public class Comment : BaseEntity
{
    public int? UserId { get; set; }
    public int PostId { get; set; }
    public required string Body { get; set; }

    //? prop de navegação para o usuário que fez o comentário
    public User? User { get; set; }

    //? prop de navegação para o post associado ao comentário
    public Post? Post { get; set; }
}
