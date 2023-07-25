using Microsoft.EntityFrameworkCore;
using ProductHub_Domain.Models;

namespace ProductHub_Infra.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
