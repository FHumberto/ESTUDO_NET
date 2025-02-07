using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;
using T_Tier.DAL.Seed;

namespace T_Tier.DAL.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    #region ================================[ENTIDADES]================================

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }

    #endregion

    //? aplica as configurações de entidades da pasta [configurations]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Seed();
    }
}
