using Flunt.Notifications;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Domain.Products;

namespace S12_PFC.Infra.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    // método para personalizar os modelos do banco
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // CHANDO A BASE PARA O IDENTITY (CLASSE PAI)
        base.OnModelCreating(builder);

        // ignora a classe notification de validação, durante a construção do banco
        builder.Ignore<Notification>();

        builder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(255);

        builder.Entity<Category>()
            .Property(c => c.Name).IsRequired();
    }

    // método que aplica convensões
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Determina que todas as propriedades do tipo string, deve conter no máximo 100 caracteres.
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
