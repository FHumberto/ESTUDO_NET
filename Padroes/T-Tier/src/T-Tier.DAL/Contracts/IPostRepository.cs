using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Contracts;

public interface IPostRepository : IRepository<Post>
{
    public Task<IReadOnlyList<Post?>> GetPostsWithUser(string id);
    public Task<Post?> GetPostByIdWithTagsAsync(int id);
    public Task<Post?> GetPostByidWithCommentsAsync(int id);
}