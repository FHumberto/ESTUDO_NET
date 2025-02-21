using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace T_Tier.API.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerRoutes(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
            {
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.ReportApiVersions = true;
            })
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static IServiceCollection AddSwaggerAuthServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            // Definição do esquema de autenticação Bearer
            setup.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira APENAS o token JWT no campo abaixo: \n\nExemplo: '{seu_token}'"
                });

            // Aplica a autenticação globalmente para todas as rotas
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer", },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });

            setup.EnableAnnotations();
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }

    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                OpenApiInfo info = new()
                {
                    Title = Assembly.GetCallingAssembly().GetName().Name,
                    Version = description.ApiVersion.ToString()
                };

                if (description.IsDeprecated)
                {
                    info.Description += "Esta Versão da API está deprecated.";
                }

                options.SwaggerDoc(description.GroupName, info);
            }
        }

        public void Configure(string? name, SwaggerGenOptions options) => Configure(options);
    }

    public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
    {
        var services = app.ApplicationServices;
        var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}