using S12_PFC.Domain.Products;
using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Categories;

public static class CategoryPost // metodo de criar
{
    public static string Template => "/categories"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    // método created
    public static IResult Action(CategoryRequest categoryRequest, AppDbContext context)
    {
        //OUTRA FORMA DE VALIDAÇÃO SEM MECHER NO MODEL OU USAR FLUNT
        //if (string.IsNullOrEmpty(categoryRequest.Name))
        //{
        //    return Results.BadRequest("Name is required");
        //}

        var category = new Category(categoryRequest.Name, "Test", "Test");

        // VALIDAÇÃO USANDO O FLUNT
        if (!category.IsValid)
        {
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails()); // PASSA OS DADOS DE ERRO PARA A EXTENSÃO
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);
    }
}
