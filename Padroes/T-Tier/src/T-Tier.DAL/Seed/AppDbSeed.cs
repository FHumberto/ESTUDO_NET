using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed
{
    public static class AppDbContextSeed
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //* Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "João", LastName = "Silva", Email = "joao.silva@example.com", PasswordHash = new byte[] { 0x20, 0x21 }, Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new User { Id = 2, FirstName = "Maria", LastName = "Oliveira", Email = "maria.oliveira@example.com", PasswordHash = new byte[] { 0x20, 0x21 }, Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new User { Id = 3, FirstName = "Carlos", LastName = "Santos", Email = "carlos.santos@example.com", PasswordHash = new byte[] { 0x20, 0x21 }, Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) }
            );

            //* Seed Posts
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, UserId = 1, Tittle = "Primeiro Post", Body = "Este é o corpo do primeiro post.", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new Post { Id = 2, UserId = 2, Tittle = "Segundo Post", Body = "Este é o corpo do segundo post.", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new Post { Id = 3, UserId = 3, Tittle = "Terceiro Post", Body = "Este é o corpo do terceiro post.", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) }
            );

            //* Seed Comments
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, UserId = 1, PostId = 1, Body = "Este é o primeiro comentário.", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new Comment { Id = 2, UserId = 2, PostId = 2, Body = "Este é o segundo comentário.", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new Comment { Id = 3, UserId = 3, PostId = 3, Body = "Este é o terceiro comentário.", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) }
            );

            //* Seed Tags
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Tecnologia", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new Tag { Id = 2, Name = "Educação", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) },
                new Tag { Id = 3, Name = "Saúde", Created = new DateTime(2025, 2, 7, 12, 0, 0), Updated = new DateTime(2025, 2, 7, 12, 0, 0) }
            );

            //* Seed PostTags
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
}
