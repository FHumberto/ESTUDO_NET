using Microsoft.AspNetCore.Identity;
using T_Tier.DAL.Contracts;

namespace T_Tier.DAL.Entities;

public class User : IdentityUser, ISoftDeleteEntity
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public bool IsDeleted { get; private set; } = false;
    
    //? Navegação para posts criados pelo usuário
    public ICollection<Post>? Posts { get; init; }

    //? Navegação para comentários feitos pelo usuário
    public ICollection<Comment>? Comments { get; init; }
    
    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}