using T_Tier.DAL.Context;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<bool> SoftDeleteAsync(User user)
    {
        user.SoftDelete();
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return true;
    }
}