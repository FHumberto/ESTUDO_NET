using Microsoft.EntityFrameworkCore;

namespace ScalarApi.Data;

public class ScalarApiDbContext : DbContext
{
    public ScalarApiDbContext(DbContextOptions<ScalarApiDbContext> options) : base(options)
    {
    }
    
    public DbSet<Persona> Personas { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonaConfiguration());
        PersonaSeed.Seed(modelBuilder);
    }
}