using Microsoft.EntityFrameworkCore;

using ScalarApiLabs.Exceptions;
using ScalarApiLabs.Helpers;
using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ScalarApiLabsDbContext _context;

    public ProductRepository(ScalarApiLabsDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResultDto<Product>> GetAllAsync(QueryFilters query)
    {
        int offset = (query.PageNumber - 1) * query.PageSize;
        int fetch = query.PageSize;

        try
        {
            var totalRecords = await _context.Products.CountAsync();

            var products = await _context.Products
                .OrderBy(p => p.Id)
                .Skip(offset)
                .Take(fetch)
                .ToListAsync();

            return new PagedResultDto<Product>(products, totalRecords, query.PageNumber, query.PageSize);
        }
        catch (Exception ex)
        {
            throw new DatabaseException($"Erro ao buscar (Produtos)", ex);
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

    public async Task<int> AddAsync(Product product)
    {
        var sql = "INSERT INTO Products (Name, Price) OUTPUT INSERTED.Id VALUES (@p0, @p1);";

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var newId = await _context.Products
                .FromSqlRaw(sql, product.Name, product.Price)  // Executa o SQL
                .Select(p => p.Id)  // Captura o ID gerado
                .FirstOrDefaultAsync();

            await transaction.CommitAsync();
            return newId;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"Erro ao gravar o (Produto): {product.Name} | [Erro]: {ex.GetType().Name} - {ex.Message}");

            await transaction.RollbackAsync();
            throw new DatabaseException($"Erro ao inserir produto: {product.Name}", ex);
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
