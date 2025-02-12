using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;

public static class PostSeed
{
    public static void SeedPosts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasData(
            new Post { Id = 1, UserId = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9", Title = "Primeiro Post", Body = "Este é o corpo do primeiro post." },
            new Post { Id = 2, UserId = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64", Title = "Segundo Post", Body = "Este é o corpo do segundo post." },
            new Post { Id = 3, UserId = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5", Title = "Terceiro Post", Body = "Este é o corpo do terceiro post." }
        );
    }
}
