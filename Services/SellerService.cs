using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Models;
using CNA_SalesWebMvc.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CNA_SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        // encontra o seller com o ID informado
        public async Task<Seller> FindByIdAsync(int id)
        {
            // eager loading carregar dois ou mais objetos
            // carrega o department e junta com o seller
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj); // remove o objeto encontrado do DBSet
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj); // adiciona o objeto ao banco de dados.
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            // verifica se não existe o objeto no banco
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj); // pode retornar uma exceção (DB update concurrence exception)
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}