using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Scalar.AspNetCore;

using ScalarApiLabs.Data;
using ScalarApiLabs.Interfaces.Persistence;
using ScalarApiLabs.Interfaces.Services;
using ScalarApiLabs.Middlewares;
using ScalarApiLabs.Models.Entities;
using ScalarApiLabs.Repositories;
using ScalarApiLabs.Services;
using ScalarApiLabs.Settings;

var builder = WebApplication.CreateBuilder(args);
var connextionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ScalarApiLabsDbContext>
    (options => options.UseSqlServer(connextionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ScalarApiLabsDbContext>()
    .AddDefaultTokenProviders();

//? quem você é?
builder.Services.AddAuthentication();
//? o que você pode fazer?
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Scalar API";
        options.Theme = ScalarTheme.DeepSpace;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
