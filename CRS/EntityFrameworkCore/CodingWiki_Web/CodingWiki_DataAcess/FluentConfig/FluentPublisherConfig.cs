using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodingWiki_DataAcess.FluentConfig;
public class FluentPublisherConfig : IEntityTypeConfiguration<Fluent_Publisher>
{
    public void Configure(EntityTypeBuilder<Fluent_Publisher> modelBuilder)
    {
        modelBuilder.HasKey(p => p.Publisher_Id);
        modelBuilder.Property(p => p.Name).IsRequired();
    }
}
