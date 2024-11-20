namespace DapperApi.Data;

using Dapper;
using DapperApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class ProdutoRepository : IProdutoRepository
{
    private readonly string _connectionString;

    public ProdutoRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Produto>> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Produto>("SELECT * FROM Produtos");
    }

    public async Task<Produto> GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Produto>("SELECT * FROM Produtos WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> Add(Produto produto)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = $"INSERT INTO Produtos (Nome, Preco, Quantidade) VALUES (@Nome, @Preco, @Quantidade)";
        return await connection.ExecuteAsync(sql, produto);
    }

    public async Task<int> Update(Produto produto)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Produtos SET Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, produto);
    }

    public async Task<int> Delete(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM Produtos WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, new { Id = id });
    }
}
