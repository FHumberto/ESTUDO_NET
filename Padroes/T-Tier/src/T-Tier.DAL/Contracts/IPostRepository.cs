using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Contracts;

public interface IPostRepository : IRepository<Post>
{
    public Task<Post?> GetPostByIdWithTagAsync(int id);
}