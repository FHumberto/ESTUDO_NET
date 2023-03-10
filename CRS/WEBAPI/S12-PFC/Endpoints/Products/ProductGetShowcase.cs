using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Products;

public class ProductGetShowcase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(AppDbContext context, int page = 1, int row = 10, string orderBy = "name")
    {
        // LIMITA O NUMERO DE ITENS MÁXIMO DE PESQUISA
        if (row > 10)
            return Results.Problem(title: "Row with max 10", statusCode: 400);

        // QUEBRA A BUSCA (E NÃO RASTREIA O OBJETO NA MEMÓRIA PARA PERFORMANCE [Já que é só consulta])
        var queryBase = context.Products.AsNoTracking().Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active);

        // ORDENA POR PREÇO OU NOME
        if (orderBy == "name")
            queryBase = queryBase.OrderBy(p => p.Name);
        else if (orderBy == "price")
            queryBase = queryBase.OrderBy(p => p.Price);
        else
            return Results.Problem(title: "Order only by price or name", statusCode: 400);

        // FAZ A PAGINAÇÃO DOS ITENS
        var queryFilter = queryBase.Skip((page - 1) * row).Take(row);

        var products = await queryFilter.ToListAsync();

        // CONVERTE PARA O PADRÃO DE RESPOSTA
        var results = products.Select(p => new ProductResponse(p.Id, p.Name, p.Category.Name, p.Description, p.HasStock, p.Price, p.Active));

        return Results.Ok(results);
    }
}