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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime localNow = DateTime.Now;

        ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry => entry.State is EntityState.Added or EntityState.Modified)
            .ToList()
            .ForEach(entry =>
            {
                //TODO: lembrar de adicionar a propriedade de [CreatedBy] e [UpdatedBy] nas entidades e adicionar aqui.
                switch (entry)
                {
                    //? se for uma entidade nova, seta a data de criação
                    case { State: EntityState.Added }:
                        entry.Entity.CreatedAt = localNow;
                        break;
                    //? se for uma entidade modificada, seta a data de atualização
                    case { State: EntityState.Modified }:
                        entry.Entity.UpdatedAt = localNow;
                        break;
                }
            });

        return base.SaveChangesAsync(cancellationToken);
    }
}