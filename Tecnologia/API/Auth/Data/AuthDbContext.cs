using Auth.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data;

public class AuthDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
