using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWiki_DataAcess.FluentConfig;
public class FluentAuthorConfig : IEntityTypeConfiguration<Fluent_Author>
{
    public void Configure(EntityTypeBuilder<Fluent_Author> modelBuilder)
    {
        modelBuilder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        modelBuilder.Property(a => a.LastName).IsRequired();
        modelBuilder.HasKey(a => a.Author_Id);
        modelBuilder.Ignore(a => a.FullName);
    }
}
