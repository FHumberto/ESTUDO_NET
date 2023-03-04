using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using S12_PFC.Endpoints.Categories;
using S12_PFC.Endpoints.Employees;
using S12_PFC.Endpoints.Security;
using S12_PFC.Infra.Data;

using System.Text;

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters() // VALIDA O TOKEN
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true, // VALIDA CILCODEVIDA
        ValidateIssuerSigningKey = true, // VALIDA CHAVE DE ASSINATURA
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"], // SE A ISSUER É A QUE TA ESPERANDO
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"], // SE A AUDENCIA É A QUE TA ESPERANDO
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

// ADICIONA A CLASSE COMO SERVIÇO
builder.Services.AddScoped<QueryAllUsersWithClaimName>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

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
app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle).WithTags("Employees");

app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle).WithTags("Security");

app.Run();