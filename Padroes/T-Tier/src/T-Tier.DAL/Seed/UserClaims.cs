using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace T_Tier.DAL.Seed;

public static class UserClaimsSeed
{
    public static void SeedUserClaims(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
            new IdentityUserClaim<string>
            {
                Id = 1,
                UserId = "a1b2c3d4-e5f6-7890-abcd-1234567890ab",
                ClaimType = "nickname",
                ClaimValue = "adminuser"
            },
            new IdentityUserClaim<string>
            {
                Id = 2,
                UserId = "a1b2c3d4-e5f6-7890-abcd-1234567890ab",
                ClaimType = "email",
                ClaimValue = "admin@example.com"
            },
            new IdentityUserClaim<string>
            {
                Id = 3,
                UserId = "b2c3d4e5-f6a7-890b-cdef-2345678901bc",
                ClaimType = "nickname",
                ClaimValue = "guestuser"
            },
            new IdentityUserClaim<string>
            {
                Id = 4,
                UserId = "b2c3d4e5-f6a7-890b-cdef-2345678901bc",
                ClaimType = "email",
                ClaimValue = "guest@example.com"
            },
            new IdentityUserClaim<string>
            {
                Id = 5,
                UserId = "c3d4e5f6-a7b8-90cd-efgh-3456789012cd",
                ClaimType = "nickname",
                ClaimValue = "default"
            },
            new IdentityUserClaim<string>
            {
                Id = 6,
                UserId = "c3d4e5f6-a7b8-90cd-efgh-3456789012cd",
                ClaimType = "email",
                ClaimValue = "default@example.com"
            }
        );
    }
}