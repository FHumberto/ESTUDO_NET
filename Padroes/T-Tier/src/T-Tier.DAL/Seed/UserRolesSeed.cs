using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace T_Tier.DAL.Seed;

public static class UserRolesSeed
{
    public static void SeedUserRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Default", NormalizedName = "DEFAULT" }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = "a1b2c3d4-e5f6-7890-abcd-1234567890ab", RoleId = "1" },
            new IdentityUserRole<string> { UserId = "b2c3d4e5-f6a7-890b-cdef-2345678901bc", RoleId = "2" },
            new IdentityUserRole<string> { UserId = "c3d4e5f6-a7b8-90cd-efgh-3456789012cd", RoleId = "2" }
        );
    }
}
