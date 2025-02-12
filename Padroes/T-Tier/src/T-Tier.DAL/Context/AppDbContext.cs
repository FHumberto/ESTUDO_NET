using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using T_Tier.DAL.Seed;

namespace T_Tier.DAL.Context;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region ================================[ENTIDADES]================================

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }

    #endregion

    //? aplica as configurações de entidades da pasta [configurations]
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        //! aplica o filtro global para todas as entidades que implementam ISoftDeleteEntity
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
            {
                MethodInfo method = typeof(AppDbContext)
                    .GetMethod(nameof(ApplySoftDeleteFilter), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, new object[] { builder });
            }
        }
        builder.Seed();
    }
    
    //! método genérico para criar o filtro de exclusão lógica
    private static void ApplySoftDeleteFilter<T>(ModelBuilder builder) where T : class, ISoftDeleteEntity
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
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