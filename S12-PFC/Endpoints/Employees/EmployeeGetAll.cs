using Microsoft.AspNetCore.Identity;

using S12_PFC.Endpoints.Categories;

namespace S12_PFC.Endpoints.Employees;

public static class EmployeeGetAll // metodo de criar
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // RECEBER O PAGE E O ROWS POR QUERY PARAMTERS
    public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager)
    {
        // FAZ A PAGINAÇÃO
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows);

        var employees = new List<EmployeeResponse>();

        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name"); // obtem o primeiro clain que tem a chave name.
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(item.Email, userName));
        }

        var employess = users.Select(u => new EmployeeResponse(u.Email, "Name")); // Convertendo a listagem para employeresponse
        return Results.Ok(employess);
    }
}
