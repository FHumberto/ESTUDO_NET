using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class DataContext : DbContext
{
    public DbSet<Produtos> Produtos { get; set; }
    public DbSet<Setores> Setores { get; set; }

    public DataContext() : base()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EST-WF-CRUD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(new Maps.ProdutosMap());
        _ = modelBuilder.ApplyConfiguration(new Maps.SetoresMap());
    }
}
