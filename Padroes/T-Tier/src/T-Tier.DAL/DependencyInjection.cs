using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using T_Tier.DAL.Context;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using T_Tier.DAL.Repositories;

namespace T_Tier.DAL;

public static class DependencyInjection
{
    public static void RegisterDALDependencies(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IPostRepository, PostsRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }
}
