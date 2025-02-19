using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class CommentRepository(AppDbContext context, ILogger<CommentRepository> logger)
    : GenericRepository<Comment>(context, logger), ICommentRepository
{
    public async Task<IReadOnlyList<Comment?>> GetCommentsWithUser(string userId)
    {
        try
        {
            var comments = await context.Comments
                .Where(c => c.UserId == userId)
                .Include(c => c.User)
                .AsNoTracking()
                .ToListAsync();

            logger.LogInformation("DAL-REPO: {Count} comentários encontrados para o usuário {UserId}.", comments.Count, userId);
            return comments;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro ao buscar comentários do usuário {UserId}.", userId);
            return new List<Comment?>();
        }
    }
}
