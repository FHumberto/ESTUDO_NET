using Microsoft.AspNetCore.Identity;
using T_Tier.DAL.Entities;

public interface IUserRepository
{
    Task<IdentityResult> SoftDeleteAsync(User user);
}