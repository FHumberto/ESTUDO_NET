using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        //? entity framework pega a PK automáticamente
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        #region ================================[RELACIONAMENTOS]================================

        //* um usuário pode criar muitos posts, mas um post pertence a um usuário 
        builder.HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);

        // Configurando o relacionamento com a entidade Comment
        //* um usuário pode fazer muitos comentários, mas um comentário pertence a um usuário
        builder.HasMany(e => e.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId);

        #endregion
    }
}
