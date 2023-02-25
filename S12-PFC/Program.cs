using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Endpoints.Categories;
using S12_PFC.Endpoints.Employees;
using S12_PFC.Infra.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionStr));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false; // personaliza a validação para não precisar de alphanumericos
    options.Password.RequireDigit = false; // não precisa de um digito
    options.Password.RequireUppercase = false; // não precisa de upercase
    options.Password.RequireLowercase = false; // não precisa de lowercase
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<AppDbContext>();

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

app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle).WithTags("Employees");
app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle).WithTags("Employees"); // ROTA EDITAR

app.Run();