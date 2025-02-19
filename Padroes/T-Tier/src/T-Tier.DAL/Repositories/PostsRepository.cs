using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class PostsRepository(AppDbContext context, ILogger<PostsRepository> logger)
    : GenericRepository<Post>(context), IPostRepository
{
    public async Task<IReadOnlyList<Post?>> GetPostsWithUser(string userId)
    {
        try
        {
            logger.LogInformation("DAL-REPO: Buscando posts do usuário {UserId}.", userId);
            var posts = await Context.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();

            logger.LogInformation("DAL-REPO: {Count} posts encontrados para o usuário {UserId}.", posts.Count, userId);
            return posts;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro ao buscar posts do usuário {UserId}.", userId);
            return [];
        }
    }

    public async Task<Post?> GetPostByIdWithTagsAsync(int id)
    {
        try
        {
            logger.LogInformation("DAL-REPO: Buscando post com ID {PostId} e suas tags.", id);
            var post = await Context.Posts
                .Include(p => p.Tags)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            logger.LogInformation("DAL-REPO: Post com ID {PostId} encontrado com {TagCount} tags.", id, post?.Tags!.Count ?? 0);
            return post;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro ao buscar post com ID {PostId}.", id);
            return null;
        }
    }

    public async Task<Post?> GetPostByidWithCommentsAsync(int id)
    {
        try
        {
            logger.LogInformation("DAL-REPO: Buscando post com ID {PostId} e seus comentários.", id);
            var post = await Context.Posts
                .Include(p => p.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            logger.LogInformation("DAL-REPO: Post com ID {PostId} encontrado com {CommentCount} comentários.", id, post?.Comments.Count ?? 0);
            return post;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro ao buscar post com ID {PostId}.", id);
            return null;
        }
    }
}
