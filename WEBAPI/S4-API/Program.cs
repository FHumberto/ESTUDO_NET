using Microsoft.AspNetCore.Mvc;

using S4_API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/", () => new { Name = "Humberto Guedes", Age = 31 });

app.MapGet("/AddHeader", (HttpResponse response) =>
{
    response.Headers.Add("Teste", "Humberto");
    return new { Name = "Humberto Guedes", Age = 31 };
});

app.MapPost("/saveproduct", (Product product) =>
{
    return $"{product.Code} - {product.Name}";
});

//? FORMAS DE PASSAR DADOS

// POR PARÂMETRO
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return $"{dateStart} - {dateEnd}";
});

// POR ROTA
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    return code;
});

// PELO HEADER
app.MapGet("/getproductbyheader", (HttpRequest request) =>
{
    return request.Headers["product-code"].ToString();
});

app.Run();

