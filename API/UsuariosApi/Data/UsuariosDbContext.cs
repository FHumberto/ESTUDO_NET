using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Domain.Models;

namespace UsuariosApi.Data;

public class UsuariosDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    public UsuariosDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notification>();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
