using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Contracts;

public interface ITagRepository : IRepository<Tag>
{
    public Task<Tag?> GetByNameAsync(string name);
    public Task<List<Tag>> GetByIdsAsync(List<int> ids);
}
