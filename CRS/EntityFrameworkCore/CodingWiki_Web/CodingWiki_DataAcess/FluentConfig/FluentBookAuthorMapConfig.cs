using CodingWiki_Model.Models;
using CodingWiki_Model.Models.FluentModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWiki_DataAcess.FluentConfig;
public class FluentBookAuthorMapConfig : IEntityTypeConfiguration<Fluent_BookAuthorMap>
{
    public void Configure(EntityTypeBuilder<Fluent_BookAuthorMap> modelBuilder)
    {
        modelBuilder.HasKey(u => new { u.Author_Id, u.Book_Id });
        modelBuilder.HasOne(u => u.Book).WithMany(u => u.BookAuthorMap)
            .HasForeignKey(u => u.Book_Id);
        modelBuilder.HasOne(u => u.Author).WithMany(u => u.BookAuthorMap)
            .HasForeignKey(u => u.Author_Id);
    }
}
