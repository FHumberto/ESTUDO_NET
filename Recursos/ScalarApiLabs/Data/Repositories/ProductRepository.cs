using Microsoft.EntityFrameworkCore;

using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ScalarApiLabsDbContext _context;

    public ProductRepository(ScalarApiLabsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            //? maneira antiga de fazer rawsql, precisando do dbset
            var products = await _context.Products.FromSqlRaw("SELECT * FROM Products").ToListAsync();
            return products;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar (Produtos) | [Erro]: {ex.GetType().Name} - {ex.Message}");
            throw;
        }
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        try
        {
            //? rawSql não mapeado (EF_8)
            return await _context.Database
                .SqlQueryRaw<Product>("SELECT Id, Name, Price FROM Products WHERE Id = @p0", id)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar o (Produto): {id} | [Erro]: {ex.GetType().Name} - {ex.Message}");
            throw;
        }
    }

    public async Task<bool> AddAsync(Product product)
    {
        var sql = "INSERT INTO Products (Name, Price) VALUES (@P0, @P1)";

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            int affectedRows = await _context.Database.ExecuteSqlRawAsync(sql, product.Name, product.Price);
            await transaction.CommitAsync();
            return affectedRows > 0;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine($"Erro ao gravar o (Produto): {product.Id} | [Erro]: {ex.GetType().Name} - {ex.Message}");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var sql = "DELETE FROM Products WHERE Id = @p0";

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            int affectedRows = await _context.Database.ExecuteSqlRawAsync(sql, id);
            await transaction.CommitAsync();
            return affectedRows > 0;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Erro ao excluir (Produto): {id} | [Erro]: {ex.GetType().Name} - {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var sql = "UPDATE Products SET Name = @p0, Price = @p1 WHERE Id = @p2";

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            int affectedRows = await _context.Database.ExecuteSqlRawAsync(sql, product.Name, product.Price, product.Id);
            await transaction.CommitAsync();
            return affectedRows > 0;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Erro ao Atualizar o (Produto): {product.Id} | [Erro]: {ex.GetType().Name} - {ex.Message}");
            return false;
        }
    }
}
