using Microsoft.AspNetCore.Mvc;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Categories;

public static class CategoryPut // metodo de editar
{
    public static string Template => "/categories/{id}"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    // método created
    public static IResult Action([FromRoute] Guid id, CategoryRequest categoryRequest, AppDbContext context)
    {
        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
        category.Name = categoryRequest.Name;
        category.Active = categoryRequest.Active;

        context.SaveChanges();

        return Results.Ok();
    }
}
