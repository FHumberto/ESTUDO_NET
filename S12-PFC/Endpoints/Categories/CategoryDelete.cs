using Microsoft.AspNetCore.Mvc;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Categories;

public static class CategoryDelete // metodo de deletar
{
    public static string Template => "/categories/{id}"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Delete.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    // método created
    public static IResult Action([FromRoute] Guid id, AppDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
        context.Categories.Remove(category);
        context.SaveChanges();

        return Results.Ok();
    }
}
