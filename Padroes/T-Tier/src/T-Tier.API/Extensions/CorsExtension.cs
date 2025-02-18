namespace T_Tier.API.Extensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {
        return services.AddCors(x
            => x.AddPolicy("Any",
                b =>
                {
                    b.AllowAnyOrigin();
                    b.AllowAnyHeader();
                    b.AllowAnyMethod();
                }
            )
        );
    }

    public static IApplicationBuilder UseCorsPolicies(this IApplicationBuilder app)
    {
        return app.UseCors("Any");
    }
}
