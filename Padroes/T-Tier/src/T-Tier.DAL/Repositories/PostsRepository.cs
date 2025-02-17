using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class PostsRepository(AppDbContext context) : GenericRepository<Post>(context), IPostRepository
{
    public async Task<IReadOnlyList<Post?>> GetPostsWithUser(string userId)
    {
        return await Context.Posts
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<Post?> GetPostByIdWithTagsAsync(int id)
    {
        return await Context.Posts
            .Include(p => p.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Post?> GetPostByidWithCommentsAsync(int id)
    {
        return await Context.Posts
            .Include(p => p.Comments)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}