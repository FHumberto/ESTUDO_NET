using Auth.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data;

public class AuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }

    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.NickName)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            NickName = "FHumberto",
            Name = "Humberto",
            Email = "Humberto@email.com",
            Password = "12345678",
            Role = "ADM"
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
