using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class TagRepository(AppDbContext context) : GenericRepository<Tag>(context), ITagRepository
{
    public async Task<Tag?> GetByNameAsync(string name)
    {
        Tag? result = await Context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Name == name);
        return result;
    }
}
