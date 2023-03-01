using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace CNA_SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync()
        {
            // retornar a lista de departamentos ordenados por nome
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}