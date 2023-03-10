using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Domain.Products;
using S12_PFC.Infra.Data;

using System.Security.Claims;

namespace S12_PFC.Endpoints.Products;

public class ProductPost
{
    public static string Template => "/products";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(ProductRequest productRequest, HttpContext http, AppDbContext context)
    {
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == productRequest.categoryId);
        var product = new Product(productRequest.Name, category, productRequest.Description, productRequest.HasStock, userId);

        // VALIDAÇÃO USANDO O FLUNT
        if (!product.IsValid)
        {
            return Results.ValidationProblem(product.Notifications.ConvertToProblemDetails()); // PASSA OS DADOS DE ERRO PARA A EXTENSÃO
        }

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return Results.Created($"/categories/{product.Id}", product.Id);
    }
}