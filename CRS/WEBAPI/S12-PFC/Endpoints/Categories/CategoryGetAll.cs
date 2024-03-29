﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Categories;

public static class CategoryGetAll // metodo de criar
{
    public static string Template => "/categories"; // indica a rota do endpoint
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() }; // para determinar os 4 métodos
    public static Delegate Handle => Action; // chama a action v

    // método de listagem

    [Authorize(Policy = "EmployeeCode")]
    public static async Task<IResult> Action(AppDbContext context)
    {
        var categories = await context.Categories.ToListAsync();

        // retorna uma nova lista do tipo (category response) || para não passar a entity
        var response = categories.Select(c => new CategoryResponse(c.Id, c.Name, c.Active));

        return Results.Ok(response);
    }
}
