using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Models;
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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        // encontra o seller com o ID informado
        public Seller FindById(int id)
        {
            // eager loading carregar dois ou mais objetos
            // carrega o department e junta com o seller
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj); // remove o objeto encontrado do DBSet
            _context.SaveChanges();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj); // adiciona o objeto ao banco de dados.
            _context.SaveChanges();
        }
    }
}