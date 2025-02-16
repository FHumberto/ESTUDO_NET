using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Repositories;

public class GenericRepository<T>(AppDbContext context) : IRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context = context;

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<int> CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task<bool> SoftDeleteAsync(T entity)
    {
        if (entity is not ISoftDeleteEntity softDeletableEntity)
        {
            throw new InvalidOperationException($"500: A entidade {typeof(T).Name} não implementa ISoftDelete.");
        }

        softDeletableEntity.SoftDelete();

        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();

        return true;
    }

}