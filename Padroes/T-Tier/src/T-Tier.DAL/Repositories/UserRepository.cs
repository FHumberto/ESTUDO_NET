using Microsoft.AspNetCore.Identity;
using T_Tier.DAL.Context;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IdentityResult> SoftDeleteAsync(User user)
    {
        user.SoftDelete();
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return IdentityResult.Success;
    }
}