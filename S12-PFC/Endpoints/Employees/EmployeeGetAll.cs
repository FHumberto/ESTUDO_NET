using S12_PFC.Infra.Data;

namespace S12_PFC.Endpoints.Employees;

public static class EmployeeGetAll // metodo de criar
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // RECEBER O PAGE E O ROWS POR QUERY PARAMTERS
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        var result = query.Execute(page.Value, rows.Value);
        return Results.Ok(result);
    }
}
