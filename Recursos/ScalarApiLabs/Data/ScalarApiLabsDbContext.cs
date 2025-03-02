using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ScalarApiLabs.Models.Entities;

namespace ScalarApiLabs.Data;

public class ScalarApiLabsDbContext(DbContextOptions<ScalarApiLabsDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScalarApiLabsDbContext).Assembly);
        modelBuilder.SeedData();
    }
}
