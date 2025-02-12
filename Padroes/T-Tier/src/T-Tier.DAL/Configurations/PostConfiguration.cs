using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Body)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.IsDeleted)
            .HasDefaultValue(false);
        
        #region ================================[RELACIONAMENTOS]================================

        //* um post pertence a um usuário, mas um usuário pode criar muitos posts
        builder.HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            //? impede a exclusão do usuário se houver posts associados
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}