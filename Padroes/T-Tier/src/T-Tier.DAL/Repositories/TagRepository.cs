using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories
{
    public class TagRepository(AppDbContext context, ILogger<TagRepository> logger)
        : GenericRepository<Tag>(context, logger), ITagRepository
    {
        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await context.Tags
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Name == name);
        }

        public async Task<List<Tag>> GetByIdsAsync(List<int> ids)
        {
            try
            {
                return await context.Tags
                    .AsNoTracking()
                    .Where(tag => ids.Contains(tag.Id))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "DAL-REPO: Erro ao buscar múltiplas tags com IDs {Ids}.", string.Join(",", ids));
                return [];
            }
        }
    }
}
