using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;

public static class UserSeed
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        PasswordHasher<User> hasher = new();

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                FirstName = "João",
                LastName = "Silva",
                Email = "joao.silva@example.com",
                NormalizedEmail = "JOAO.SILVA@EXAMPLE.COM",
                UserName = "joao.silva",
                NormalizedUserName = "JOAO.SILVA",
                PasswordHash = hasher.HashPassword(null!, "P@ssword1"),
            },
            new User
            {
                Id = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                FirstName = "Maria",
                LastName = "Oliveira",
                Email = "maria.oliveira@example.com",
                NormalizedEmail = "MARIA.OLIVEIRA@EXAMPLE.COM",
                UserName = "maria.oliveira",
                NormalizedUserName = "MARIA.OLIVEIRA",
                PasswordHash = hasher.HashPassword(null!, "P@ssword2"),
            },
            new User
            {
                Id = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                FirstName = "Carlos",
                LastName = "Santos",
                Email = "carlos.santos@example.com",
                NormalizedEmail = "CARLOS.SANTOS@EXAMPLE.COM",
                UserName = "carlos.santos",
                NormalizedUserName = "CARLOS.SANTOS",
                PasswordHash = hasher.HashPassword(null!, "P@ssword3"),
            }
        );
    }
}
