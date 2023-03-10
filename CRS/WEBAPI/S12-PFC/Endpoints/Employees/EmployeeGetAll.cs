using Microsoft.AspNetCore.Authorization;

using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Employees;

public static class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    // RECEBER O PAGE E O ROWS POR QUERY PARAMTERS
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}