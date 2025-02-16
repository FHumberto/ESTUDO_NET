using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class CommentRepository(AppDbContext context) : GenericRepository<Comment>(context), ICommentRepository
{
    public async Task<IReadOnlyList<Comment?>> GetCommentsWithUser(string userId)
    {
        return await Context.Comments
            .Where(p => p.UserId == userId)
            .Include(p => p.User)
            .AsNoTracking()
            .ToListAsync();
    }
}