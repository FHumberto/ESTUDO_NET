using Microsoft.EntityFrameworkCore;

using Scalar.AspNetCore;

using ScalarApiLabs.Data;
using ScalarApiLabs.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connextionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<ScalarApiLabsDbContext>
    (options => options.UseSqlServer(connextionString));

var app = builder.Build();

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
app.UseAuthorization();
app.MapControllers();

app.Run();
