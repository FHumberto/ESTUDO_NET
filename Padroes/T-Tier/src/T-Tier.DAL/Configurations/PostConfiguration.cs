using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Tittle)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Body)
            .IsRequired()
            .HasMaxLength(255);

        #region ================================[RELACIONAMENTOS]================================

        //* um post pertence a um usuário, mas um usuário pode criar muitos posts
        builder.HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

        //* um post pode ter vários comentários, mas um comentário pertence a um único post
        builder.HasMany(c => c.Comments)
            .WithOne(p => p.Post)
            .HasForeignKey(c => c.PostId);

        //! essa etapa cria tabela intermédiária da relacção M <==> M
        //* um post pode ter muitas tags, e uma tag pode estar associada a muitos posts
        builder.HasMany(t => t.Tags)
            .WithMany(p => p.Posts)
            .UsingEntity<Dictionary<string, object>>
                ("PostTag",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<Post>().WithMany().HasForeignKey("PostId")
                );

        #endregion
    }
}
