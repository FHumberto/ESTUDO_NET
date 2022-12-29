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

        public void Insert(Seller obj)
        {
            _context.Add(obj); // adiciona o objeto ao banco de dados.
            _context.SaveChanges();
        }
    }
}