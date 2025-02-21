using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;

public static class TagSeed
{
    public static void SeedTags(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>().HasData(
            new Tag { Id = 1, Name = "Tecnologia" },
            new Tag { Id = 2, Name = "Educação" },
            new Tag { Id = 3, Name = "Saúde" }
        );
    }
}