using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScalarApiLabs.Data.EntitiesConfigurations;

public class IdentityConfiguration : IEntityTypeConfiguration<IdentityRole>,
                                     IEntityTypeConfiguration<IdentityUserRole<string>>,
                                     IEntityTypeConfiguration<IdentityUserClaim<string>>,
                                     IEntityTypeConfiguration<IdentityUserLogin<string>>,
                                     IEntityTypeConfiguration<IdentityUserToken<string>>,
                                     IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.ToTable("AspNetRoles", "Account");
    }

    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.ToTable("AspNetUserRoles", "Account");
    }

    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.ToTable("AspNetUserClaims", "Account");
    }

    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.ToTable("AspNetUserLogins", "Account");
    }

    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable("AspNetUserTokens", "Account");
    }

    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.ToTable("AspNetRoleClaims", "Account");
    }
}
