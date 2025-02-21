using Microsoft.EntityFrameworkCore;

namespace T_Tier.DAL.Seed;

public static class AppDbContextSeed
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.SeedUsers();
        modelBuilder.SeedUserRoles();
        modelBuilder.SeedUserClaims();
        modelBuilder.SeedPosts();
        modelBuilder.SeedComments();
        modelBuilder.SeedTags();
        modelBuilder.SeedPostTags();
    }
}
