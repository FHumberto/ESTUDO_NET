using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Models;

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
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
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