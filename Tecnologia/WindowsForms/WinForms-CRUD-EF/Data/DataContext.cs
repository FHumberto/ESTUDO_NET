using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;
internal class DataContext : DbContext
{
    public DbSet<Produtos> Produtos { get; set; }

    public DataContext() : base()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProdutosDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Maps.ProdutosMap());
    }
}
