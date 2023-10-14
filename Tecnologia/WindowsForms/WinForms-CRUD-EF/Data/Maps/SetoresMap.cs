using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps;
public class SetoresMap : IEntityTypeConfiguration<Setores>
{
    public void Configure(EntityTypeBuilder<Setores> builder)
    {
        builder.ToTable("Setores");
        builder.HasKey(x => x.IdSetor);
        builder.Property(x => x.Descricao).HasColumnType("varchar").HasMaxLength(50);
    }
}
