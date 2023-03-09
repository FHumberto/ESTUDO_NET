using Microsoft.AspNetCore.Mvc;

using S12_PFC.Infra.Data;

using static System.Net.WebRequestMethods;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace S12_PFC.Endpoints.Categories;

public static class CategoryPut // metodo de editar
{
    public static string Template => "/categories/{id:guid}"; // VALIDAÇÃO PELA ROTA, INFORMANDO O TIPO
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    [Authorize(Policy = "EmployeePolicy")]
    // método created
    public static async Task<IResult> Action([FromRoute] Guid id, HttpContext http, CategoryRequest categoryRequest, AppDbContext context)
    {

        // PEGA OS DADOS O NOME QUE ESTÁ NO TOKEN
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var category = await context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();

        // VALIDAÇÃO SE NÃO ENCONTRE NO BANCO DE DADOS
        if (category == null)
            return Results.NotFound();

        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails()); // FAZ A VALIDAÇÃO BASEADO NO MODEL

        await context.SaveChangesAsync();

        return Results.Ok();
    }
}
