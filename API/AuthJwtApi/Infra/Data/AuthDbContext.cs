using AuthJwtApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthJwtApi.Infra.Data;

public class AuthDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.UserName)
            .IsUnique();
    }
}
