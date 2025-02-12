using Microsoft.EntityFrameworkCore;

namespace T_Tier.DAL.Seed;

public static class PostTagSeed
{
    public static void SeedPostTags(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity("PostTag").HasData(
            new { PostId = 1, TagId = 1 },
            new { PostId = 1, TagId = 2 },
            new { PostId = 2, TagId = 2 },
            new { PostId = 2, TagId = 3 },
            new { PostId = 3, TagId = 1 },
            new { PostId = 3, TagId = 3 }
        );
    }
}
