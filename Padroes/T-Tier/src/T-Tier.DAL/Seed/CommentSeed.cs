using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;


public static class CommentSeed
{
    public static void SeedComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                Id = 1,
                UserId = "b2c3d4e5-f6a7-890b-cdef-2345678901bc", // guestuser
                PostId = 1,
                Body = "Ótimo post, admin!"
            },
            new Comment
            {
                Id = 2,
                UserId = "c3d4e5f6-a7b8-90cd-efgh-3456789012cd", // default
                PostId = 2,
                Body = "Interessante ponto de vista!"
            },
            new Comment
            {
                Id = 3,
                UserId = "a1b2c3d4-e5f6-7890-abcd-1234567890ab", // adminuser
                PostId = 3,
                Body = "Parabéns pelo post!"
            }
        );
    }
}
