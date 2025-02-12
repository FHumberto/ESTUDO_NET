using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Define [blog] como o schema do Comments
        builder.ToTable("Comments", "blog");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Body)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);

        #region ================================[RELACIONAMENTOS]================================

        //* um comentário está associado a um usuário, mas um usuário pode fazer muitos comentários
        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            //? impede a exclusão do usuário se houver comentários vinculados
            .OnDelete(DeleteBehavior.Restrict);

        //* um comentário tem um post associado, mas um post pode ter muitos comentários
        builder.HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            //* restrict impede a exclusão do post se houver comentários vinculados
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}
