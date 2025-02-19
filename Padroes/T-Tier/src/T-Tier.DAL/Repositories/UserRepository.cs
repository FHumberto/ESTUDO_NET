using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class UserRepository(AppDbContext context, ILogger<UserRepository> logger) : IUserRepository
{
    /// <summary>
    /// Marca um usuário com excluido sem removelo do banco de dados
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> SoftDeleteAsync(User user)
    {
        try
        {
            user.SoftDelete();
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogError(dbEx, "DAL-REPO: Erro ao atualizar banco de dados durante soft delete do usuário {UserId}.", user.Id);
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro inesperado ao realizar soft delete do usuário {UserId}.", user.Id);
            return false;
        }
    }

    //? Teste Utilizando RawSQL do EntityFramework
    /// <summary>
    /// Remove o usuário e todas as suas dependências (comentários e postagens associadas ao usuário)
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteUserWithDependenciesAsync(string userId)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            logger.LogInformation("Iniciando remoção de dependências do usuário {UserId}.", userId);

            #region 1. REMOVER COMENTÁRIOS DO USUÁRIO

            int deletedComments = await context.Database.ExecuteSqlRawAsync(
                "DELETE FROM blog.Comments WHERE UserId = {0}", userId);

            logger.LogInformation("Removidos {DeletedComments} comentários do usuário {UserId}.", deletedComments, userId);

            #endregion

            #region 2. REMOVER POSTS DO USUÁRIO

            int deletedPosts = await context.Database.ExecuteSqlRawAsync(
                "DELETE FROM blog.Posts WHERE UserId = {0}", userId);
            logger.LogInformation("Removidos {DeletedPosts} posts do usuário {UserId}.", deletedPosts, userId);

            #endregion

            #region 3. REMOVER O USUÁRIO

            int deletedUsers = await context.Database.ExecuteSqlRawAsync(
                "DELETE FROM [Identity].[AspNetUsers] WHERE Id = {0}", userId);

            logger.LogInformation("Usuário {UserId} removido com sucesso.", userId);

            await context.SaveChangesAsync();

            #endregion

            await transaction.CommitAsync();

            logger.LogInformation("Usuário {UserId} excluído com sucesso.", userId);

            return true;
        }
        catch (DbUpdateException dbEx)
        {
            await transaction.RollbackAsync();
            logger.LogError(dbEx, "DAL-REPO: Erro ao excluir dependências do usuário {UserId}. Operação revertida.", userId);
            return false;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "DAL-REPO: Erro inesperado ao excluir o usuário {UserId}. Operação revertida.", userId);
            return false;
        }
    }
}