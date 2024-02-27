using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.DbContext;
using HR.LeaveManagement.Identity.Models;
using HR.LeaveManagement.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration) 
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        #region DATABASE
        
        //? se necessário usar um único sistema de login, definir um DB separado (outra connection string)
        services.AddDbContext<HrLeaveManagementIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString")));

        //? adiciona os serviços que estão armazenado no banco do identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<HrLeaveManagementIdentityDbContext>()
            .AddDefaultTokenProviders();

        #endregion

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();

        #region AUTENTICAÇÃO

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        //! define as regras de como o bearer deve ser validado
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, //? deve possuir uma key válida
                ValidateIssuer = true, //? verifica se o emissor do token é válido
                ValidateAudience = true, //? verifica se o destinatário do token é válido
                ValidateLifetime = true, //? verifica se o token está dentro do período de validade
                ClockSkew = TimeSpan.Zero, //? define a tolerância de tempo de expiração do relógio (0 segundos)
                ValidIssuer = configuration["JwtSettings:Issuer"], //? define o emissor válido do token
                ValidAudience = configuration["JwtSettings:Audience"], //? define o destinatário válido do token
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });

        #endregion

        return services;
    }
}
