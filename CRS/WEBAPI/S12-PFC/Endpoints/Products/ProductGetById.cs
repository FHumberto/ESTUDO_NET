using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Products;

public class ProductGetById
{
    public static string Template => "/products/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, HttpContext http, AppDbContext context)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return Results.NotFound();

        return Results.Ok(product);
    }
}