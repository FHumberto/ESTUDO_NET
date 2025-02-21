using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Entities;

namespace T_Tier.DAL.Seed;

public static class UserSeed
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "a1b2c3d4-e5f6-7890-abcd-1234567890ab",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                PasswordHash = "AQAAAAIAAYagAAAAEFL1NcDnXQQmlE9E+oiCQRABamOOBcwBdvPBzNNLDB+8Qv6IGbmvag0qsLxdswDDSw==",
                SecurityStamp = "1234567890abcdef",
                ConcurrencyStamp = "abcdef1234567890"
            },
            new User
            {
                Id = "b2c3d4e5-f6a7-890b-cdef-2345678901bc",
                FirstName = "Guest",
                LastName = "User",
                Email = "guest@example.com",
                NormalizedEmail = "GUEST@EXAMPLE.COM",
                UserName = "guestuser",
                NormalizedUserName = "GUESTUSER",
                PasswordHash = "AQAAAAIAAYagAAAAEJwHp5n8U4kmadRHRZ8MO0jjj7YHkU0MgrCEsZzotYy2zf9YPIaNAd7IrqKXJv/ZRQ==",
                SecurityStamp = "abcdef1234567890",
                ConcurrencyStamp = "1234567890abcdef"
            },
            new User
            {
                Id = "c3d4e5f6-a7b8-90cd-efgh-3456789012cd",
                FirstName = "Default",
                LastName = "User",
                Email = "default@example.com",
                NormalizedEmail = "DEFAULT@EXAMPLE.COM",
                UserName = "default",
                NormalizedUserName = "DEFAULT",
                PasswordHash = "AQAAAAIAAYagAAAAELCoiOQvm1OPWvY4aORXf/esrsCcwzFERM55w5Bb1WSvMUnKhKOQuDhsIrBdpQqseA==",
                SecurityStamp = "fedcba0987654321",
                ConcurrencyStamp = "0987654321fedcba"
            }
        );
    }
}
