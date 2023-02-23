using Microsoft.EntityFrameworkCore;

using S12_PFC.Endpoints.Categories;
using S12_PFC.Infra.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStr));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/", () => "Hello World!");

//// (url, metodo, ação)
app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle).WithTags("Categories"); // ROTA CREATE
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle).WithTags("Categories"); // ROTA LISTA
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle).WithTags("Categories"); // ROTA EDITAR
app.MapMethods(CategoryDelete.Template, CategoryDelete.Methods, CategoryDelete.Handle).WithTags("Categories"); // ROTA EDITAR


app.Run();