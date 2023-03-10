﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Products;

public class ProductGetAll
{
    public static string Template => "/products";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(AppDbContext context)
    {
        var products = context.Products.Include(p => p.Category).OrderBy(p => p.Name).ToList();
        var results = products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Active));
        return Results.Ok(results);
    }
}