namespace T_Tier.DAL.Entities;

public class Tag : BaseEntity
{
    public required string Name { get; init; }

    //! o entity framework cria automaticamente a tabela PostTags
    //? prop de navegação para os posts associados a tag
    public ICollection<Post>? Posts { get; init; }
}
