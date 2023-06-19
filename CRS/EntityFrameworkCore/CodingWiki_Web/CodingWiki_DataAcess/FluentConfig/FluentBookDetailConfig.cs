using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWiki_DataAcess.FluentConfig;
public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
{
    public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
    {
        modelBuilder.ToTable("Fluent_BookDetails");
        modelBuilder.Property(u => u.NumberOfCharpters).HasColumnName("NoOfCharpters").IsRequired();
        modelBuilder.HasKey(u => u.BookDetail_Id);

        //! relação 1 para 1 com Book
        modelBuilder.HasOne(b => b.Book).WithOne(b => b.BookDetail)
            .HasForeignKey<Fluent_BookDetail>(u => u.Book_Id);
    }
}
