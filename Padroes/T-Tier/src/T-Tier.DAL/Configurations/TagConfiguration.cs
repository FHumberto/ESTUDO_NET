using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        // Define [blog] como o schema da tabela Tags
        builder.ToTable("Tags", "blog");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        //? config para garantir que o nome da tag seja único
        builder.HasIndex(t => t.Name)
            .IsUnique();

        #region ================================[RELACIONAMENTOS]================================

        //* uma tag pode estar associada a muitos posts, e um post pode ter muitas tags
        builder.HasMany(t => t.Posts)
            .WithMany(p => p.Tags)
            .UsingEntity<Dictionary<string, object>>
                ("PostTag",
                j => j.HasOne<Post>().WithMany().HasForeignKey("PostId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.Cascade)
                );

        #endregion
    }
}
