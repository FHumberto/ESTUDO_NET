using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class TagRepository(AppDbContext context, ILogger<TagRepository> logger) : GenericRepository<Tag>(context, logger), ITagRepository
{
    public async Task<Tag?> GetByNameAsync(string name)
    {
        Tag? result = await context.Tags
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Name == name);
        return result;
    }
}
