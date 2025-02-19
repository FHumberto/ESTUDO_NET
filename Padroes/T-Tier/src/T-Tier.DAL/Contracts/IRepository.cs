using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Contracts;

public interface IRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<int> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<bool> SoftDeleteAsync(T entity);
}
