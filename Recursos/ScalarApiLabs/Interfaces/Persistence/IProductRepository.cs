using ScalarApiLabs.Helpers;
using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data.Repositories;

public interface IProductRepository
{
    Task<PagedResultDto<Product>> GetAllAsync(QueryFilters query);
    Task<Product?> GetByIdAsync(int id);
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
