using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class PostsRepository(AppDbContext context) : GenericRepository<Post>(context), IPostRepository
{
    public override async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}