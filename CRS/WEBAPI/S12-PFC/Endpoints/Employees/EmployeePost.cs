using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace S12_PFC.Endpoints.Employees;

public static class EmployeePost // metodo de criar
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserManager<IdentityUser> userManager)
    {

        // PEGA OS DADOS O NOME QUE ESTÁ NO TOKEN
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var newUser = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = await userManager.CreateAsync(newUser, employeeRequest.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.Errors.ConvertToProblemDetails());
        }

        // AGRUPANDO AS CLAIM EM UMA LISTA
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId), // PEGA PELA CLAIM QUEM FOI O USUÁRIO
        };

        var claimResult = await userManager.AddClaimsAsync(newUser, userClaims);


        if (!claimResult.Succeeded)
        {
            return Results.BadRequest(claimResult.Errors.ConvertToProblemDetails());
        }

        return Results.Created($"/categories/{newUser.Id}", newUser.Id);
    }
}