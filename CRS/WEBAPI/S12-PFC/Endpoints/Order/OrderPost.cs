using Microsoft.AspNetCore.Authorization;

using S12_PFC.Endpoints.Order;
using S12_PFC.Infra.Data;

using System.Security.Claims;

namespace S12_PFC.Endpoints.Clients;

public static class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest, HttpContext http, AppDbContext context)
    {
        // PEGA OS DADOS DO CLIENTE AUTENTICADO (HTTP)
        var clientId = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims
            .First(c => c.Type == "Name").Value;

        // PERCORRE O BANCO PELOS PRODUTOS NO PEDIDO (EVITANDO O ACESSO POR LOOP FOREACH)
        var productsFound = context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();

        //metodo de baixo desempenho
        //foreach (var item in orderRequest.ProductIds)
        //{
        //    var product = context.Products.First(p => p.Id == item);
        //    products.Add(product);
        //}

        // CONSTROI O PEDIDO
        var order = new S12_PFC.Domain.Order.Order(clientId, clientName, productsFound, orderRequest.DeliveryAddress);

        // VALIDAÇÃO
        if (!order.IsValid)
        {
            return Results.ValidationProblem(order.Notifications.ConvertToProblemDetails());
        }

        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}", order.Id);
    }
}