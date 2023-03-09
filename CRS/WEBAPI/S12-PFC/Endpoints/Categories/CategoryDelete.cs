using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Categories;

public static class CategoryDelete // metodo de deletar
{
    public static string Template => "/categories/{id}"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action([FromRoute] Guid id, AppDbContext context)
    {
        var category = await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();
        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
