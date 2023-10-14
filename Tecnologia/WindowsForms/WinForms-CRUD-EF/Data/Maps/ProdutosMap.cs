using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Maps;
public class ProdutosMap : IEntityTypeConfiguration<Produtos>
{
    public void Configure(EntityTypeBuilder<Produtos> builder)
    {
        builder.ToTable("Produtos");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Descricao).IsRequired().HasColumnType("varchar").HasMaxLength(70);
        builder.Property(x => x.Un).IsRequired().HasColumnType("varchar").HasMaxLength(3);
        builder.Property(x => x.Unitario).IsRequired().HasColumnType("decimal").HasPrecision(10, 2);
        builder.Property(x => x.IdSetor).HasColumnType("int");
    }
}
