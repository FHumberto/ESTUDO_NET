using Flunt.Notifications;

using Microsoft.EntityFrameworkCore;

using S12_PFC.Domain.Products;

namespace S12_PFC.Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // método para personalizar os modelos do banco
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ignora a classe notification de validação, durante a construção do banco
        modelBuilder.Ignore<Notification>();

        modelBuilder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name).IsRequired();
    }

    // método que aplica convensões
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Determina que todas as propriedades do tipo string, deve conter no máximo 100 caracteres.
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
