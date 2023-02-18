using Microsoft.AspNetCore.Mvc;

using S5_CRUD.Data;
using S5_CRUD.Model;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// CREATE
app.MapPost("/saveproduct", (Product product) =>
{
    ProductRepository.Add(product);
    return $"{product.Name} saved";
});

// READ
app.MapGet("/getproduct/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    return product;
});

// EDIT
app.MapPut("/editproduct", (Product product) =>
{
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return $"{product.Name} atualizado!";
});

// DELETE
app.MapDelete("/getproduct/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    ProductRepository.Remove(product);
    return "Produto removido";
});


app.Run();