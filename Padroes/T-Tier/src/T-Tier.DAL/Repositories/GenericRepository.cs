using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class GenericRepository<T>(AppDbContext context, ILogger<GenericRepository<T>> logger) : IRepository<T> where T : BaseEntity
{
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        try
        {
            return await context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro ao buscar todos os registros da entidade {EntityName}.", typeof(T).Name);
            return [];
        }
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        try
        {
            return await context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "DAL-REPO: Erro ao buscar a entidade {EntityName} com ID {Id}.", typeof(T).Name, id);
            return null;
        }
    }

    public async Task<int> CreateAsync(T entity)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return entity.Id;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "DAL-REPO: Erro ao criar entidade {EntityName}.", typeof(T).Name);
            return 0;
        }
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "DAL-REPO: Erro ao deletar entidade {EntityName} com ID {Id}.", typeof(T).Name, entity.Id);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "DAL-REPO: Erro ao atualizar entidade {EntityName} com ID {Id}.", typeof(T).Name, entity.Id);
            return false;
        }
    }

    public async Task<bool> SoftDeleteAsync(T entity)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            if (entity is not ISoftDeleteEntity softDeletableEntity)
            {
                var message = $"A entidade {typeof(T).Name} não implementa ISoftDelete.";
                logger.LogWarning("DAL-REPO: {Message}", message);
                throw new InvalidOperationException($"500: {message}");
            }

            softDeletableEntity.SoftDelete();
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "DAL-REPO: Erro ao realizar soft delete na entidade {EntityName} com ID {Id}.", typeof(T).Name, entity.Id);
            return false;
        }
    }
}
