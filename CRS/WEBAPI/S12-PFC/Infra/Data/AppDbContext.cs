using Flunt.Notifications;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Domain.Order;
using S12_PFC.Domain.Products;

namespace S12_PFC.Infra.Data;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

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
        builder.Entity<Product>()
            .Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();

        builder.Entity<Category>()
            .Property(c => c.Name).IsRequired();

        builder.Entity<Order>()
            .Property(c => c.ClientId).IsRequired();
        builder.Entity<Order>()
            .Property(o => o.DeliveryAddress).IsRequired();
        builder.Entity<Order>()
            .Ignore(o => o.Name);

        //RELACIONAMENTO PEDIDO E PRODUTOS M <-> M
        builder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity(x => x.ToTable("OrderProducts"));
    }

    // método que aplica convensões
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Determina que todas as propriedades do tipo string, deve conter no máximo 100 caracteres.
        configurationBuilder.Properties<string>().HaveMaxLength(100);
    }
}
