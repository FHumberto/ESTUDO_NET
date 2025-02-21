using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;

public static class PostSeed
{
    public static void SeedPosts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = 1,
                UserId = "a1b2c3d4-e5f6-7890-abcd-1234567890ab", // adminuser
                Title = "Admin's First Post",
                Body = "Este é um post do administrador."
            },
            new Post
            {
                Id = 2,
                UserId = "b2c3d4e5-f6a7-890b-cdef-2345678901bc", // guestuser
                Title = "Guest's First Post",
                Body = "Este é um post do convidado."
            },
            new Post
            {
                Id = 3,
                UserId = "c3d4e5f6-a7b8-90cd-efgh-3456789012cd", // default
                Title = "Default's First Post",
                Body = "Este é um post do usuário padrão."
            }
        );
    }
}
