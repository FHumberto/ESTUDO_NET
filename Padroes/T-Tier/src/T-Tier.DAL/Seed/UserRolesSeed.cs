using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace T_Tier.DAL.Seed;

public static class UserRolesSeed
{
    public static void SeedUserRoles(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Default",
                NormalizedName = "DEFAULT"
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "e3d46b61-39c2-4fd3-b36b-2a30c2c2c3e9",
                RoleId = "1"
            },
            new IdentityUserRole<string>
            {
                UserId = "c7d96d38-45b1-4a3a-8a4d-746e4c929f64",
                RoleId = "2"
            },
            new IdentityUserRole<string>
            {
                UserId = "b2fcd97a-91b4-43a0-b55f-d1d5e7a8b7b5",
                RoleId = "2"
            }
        );
    }
}