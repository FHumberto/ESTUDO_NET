using CNA_SalesWebMvc.Data;
using CNA_SalesWebMvc.Models;

namespace CNA_SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            // retornar a lista de departamentos ordenados por nome
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}