using Microsoft.EntityFrameworkCore;
using ScalarApi.Data;

namespace ScalarApi.Services;

public class PersonaRepository
{
    private readonly ScalarApiDbContext _context;

    public PersonaRepository(ScalarApiDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Persona>> GetPersonasAsync()
    {
        return await _context.Personas.ToListAsync();
    }
}