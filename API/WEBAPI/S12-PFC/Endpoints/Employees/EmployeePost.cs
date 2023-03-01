using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

namespace S12_PFC.Endpoints.Employees;

public static class EmployeePost // metodo de criar
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors.ConvertToProblemDetails());
        }

        // AGRUPANDO AS CLAIM EM UMA LISTA
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;


        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(claimResult.Errors.ConvertToProblemDetails());
        }

        return Results.Created($"/categories/{user.Id}", user.Id);
    }
}
