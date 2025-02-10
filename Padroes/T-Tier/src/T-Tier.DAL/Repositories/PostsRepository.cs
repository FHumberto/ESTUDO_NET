using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class PostsRepository(AppDbContext context) : GenericRepository<Post>(context), IPostRepository
{
    public async Task<Post?> GetPostByIdWithTagAsync(int id)
    {
        return await Context.Posts
            .Include(p => p.Tags)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}