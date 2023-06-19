using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWiki_DataAcess.FluentConfig;
public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
{
    public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
    {
        modelBuilder.Property(u => u.ISBN).IsRequired().HasMaxLength(50);
        modelBuilder.HasKey(u => u.BookId);
        modelBuilder.Ignore(u => u.PriceRange);

        //! relacão 1 para many com publisher
        modelBuilder.HasOne(p => p.Publisher).WithMany(b => b.Books)
            .HasForeignKey(p => p.Publisher_Id);
    }
}
