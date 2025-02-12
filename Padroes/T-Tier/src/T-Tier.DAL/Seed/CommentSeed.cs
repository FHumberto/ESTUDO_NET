using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;

public static class CommentSeed
{
    public static void SeedComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().HasData(
            new Comment { Id = 1, UserId = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9", PostId = 1, Body = "Este é o primeiro comentário." },
            new Comment { Id = 2, UserId = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64", PostId = 2, Body = "Este é o segundo comentário." },
            new Comment { Id = 3, UserId = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5", PostId = 3, Body = "Este é o terceiro comentário." }
        );
    }
}
