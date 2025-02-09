using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context) { }

    public async Task<Tag?> GetByNameAsync(string name)
    {
        var result = await _context.Tags
            .FirstOrDefaultAsync(q => q.Name == name);
        return result;
    }
}
