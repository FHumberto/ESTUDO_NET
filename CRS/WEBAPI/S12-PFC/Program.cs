using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using S12_PFC.Domain.Users;
using S12_PFC.Endpoints.Categories;
using S12_PFC.Endpoints.Clients;
using S12_PFC.Endpoints.Employees;
using S12_PFC.Endpoints.Products;
using S12_PFC.Endpoints.Security;
using S12_PFC.Infra.Data;

using Serilog;
using Serilog.Sinks.MSSqlServer;

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

// PROTEGE TODAS AS ROTAS, QUE NÃO TEM AUTHORIZE
builder.Services.AddAuthorization(options =>
{
    // POLITICA QUE INFORMA QUE TODOS OS ENDPOINTS PRECISAM DE AUTORIZAÇÃO
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser() // obriga o usuário a está autenticado
        .Build();

    // PARA O USER ACESSAR O ENDPOINT NESSA POLICE ELE DEVE TER UM EMPLOYECODE
    options.AddPolicy("EmployeePolicy", p =>
        p.RequireAuthenticatedUser().RequireClaim("EmployeeCode"));

    //// EXEMPLO DE REGRA PARA EMPLOYE 005
    //options.AddPolicy("Employe005Policy", p =>
    //p.RequireAuthenticatedUser().RequireClaim("EmployeeCode", "005"));
});

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
        ClockSkew = TimeSpan.Zero, // DA ZERO TEMPO DE BONUS QUANDO O TOKEN EXPIRAR, QUANDO ACABA O TEMPO ACABOU
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"], // SE A ISSUER É A QUE TA ESPERANDO
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"], // SE A AUDENCIA É A QUE TA ESPERANDO
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

// ADICIONA A CLASSE COMO SERVIÇO
builder.Services.AddScoped<QueryAllUsersWithClaimName>();
builder.Services.AddScoped<UserCreator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
}); //Swagger Service

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo.Console()
        .WriteTo.MSSqlServer(
            context.Configuration.GetConnectionString("DefaultConnection"),
              sinkOptions: new MSSqlServerSinkOptions()
              {
                  AutoCreateSqlTable = true,
                  TableName = "LogAPI"
              });
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.UseHttpsRedirection();

//// (url, metodo, ação)
// ROTAS DE CATEGORIA
app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle).WithTags("Categories"); // ROTA CREATE
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle).WithTags("Categories"); // ROTA LISTA
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle).WithTags("Categories"); // ROTA EDITAR
app.MapMethods(CategoryDelete.Template, CategoryDelete.Methods, CategoryDelete.Handle).WithTags("Categories"); // ROTA EDITAR

//ROTAS DE PRODUTO
app.MapMethods(ProductPost.Template, ProductPost.Methods, ProductPost.Handle).WithTags("Products");
app.MapMethods(ProductGetAll.Template, ProductGetAll.Methods, ProductGetAll.Handle).WithTags("Products");
app.MapMethods(ProductGetById.Template, ProductGetById.Methods, ProductGetById.Handle).WithTags("Products");
app.MapMethods(ProductGetShowcase.Template, ProductGetShowcase.Methods, ProductGetShowcase.Handle).WithTags("Products");

// ROTAS DE CLIENTE
app.MapMethods(ClientPost.Template, ClientPost.Methods, ClientPost.Handle).WithTags("Clients");

// ROTAS ADM
app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle).WithTags("Employees");
app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle).WithTags("Employees");

// ROTAS DE SEG
app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle).WithTags("Security");


//FAZ O TRATAMENTO DE EXCEPTION
app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext http) =>
{
    var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error; // determina o tipo de erro

    if (error != null)
    {
        if (error is SqlException)
            return Results.Problem(title: "Database out", statusCode: 500);
        else if (error is BadHttpRequestException)
            return Results.Problem(title: "Error to convert data to other type. See all the information sent", statusCode: 500);
    }

    return Results.Problem(title: "An error ocurred", statusCode: 500);
});

app.Run();