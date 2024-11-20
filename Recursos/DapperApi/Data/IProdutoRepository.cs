using DapperApi.Models;

namespace DapperApi.Data;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAll();
    Task<Produto> GetById(int id);
    Task<int> Add(Produto produto);
    Task<int> Update(Produto produto);
    Task<int> Delete(int id);
}
