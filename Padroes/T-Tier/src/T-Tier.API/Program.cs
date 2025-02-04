using Microsoft.EntityFrameworkCore;
using T_Tier.DAL.Context;

var builder = WebApplication.CreateBuilder(args);
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddControllers();

builder.Services
    .AddOpenApi();

builder.Services
    .AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options 
        => options.SwaggerEndpoint("/openapi/v1.json", "Three Layer API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
