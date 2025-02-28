using Microsoft.EntityFrameworkCore;

using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data.Repositories;

public class ProductRepository
{
    private readonly ScalarApiLabsDbContext _context;

    public ProductRepository(ScalarApiLabsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var products = await _context.Products.FromSqlRaw("SELECT * FROM Products").ToListAsync();
            await transaction.CommitAsync();
            return products;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
