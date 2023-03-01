using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace CNA_SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            // constroi um objeto resultado do tipo iquerable
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) // junta a tabela seller
                .Include(x => x.Seller.Department) // junta a tabela departamento
                .OrderBy(x => x.Date) // ordena por data
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department?, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            // constroi um objeto resultado do tipo iquerable no banco
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) // junta a tabela seller
                .Include(x => x.Seller.Department) // junta a tabela departamento
                .OrderBy(x => x.Date) // ordena por data
                .GroupBy(x => x.Seller.Department) // muda o tipo para agrupamento
                .ToListAsync();
        }
    }
}