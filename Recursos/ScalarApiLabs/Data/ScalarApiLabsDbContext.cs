using Microsoft.EntityFrameworkCore;

using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data;

public class ScalarApiLabsDbContext : DbContext
{
    public ScalarApiLabsDbContext(DbContextOptions<ScalarApiLabsDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScalarApiLabsDbContext).Assembly);
        modelBuilder.SeedData();
    }
}
