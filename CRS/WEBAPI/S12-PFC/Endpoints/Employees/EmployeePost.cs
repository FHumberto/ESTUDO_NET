using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using S12_PFC.Domain.Users;

using System.Security.Claims;

namespace S12_PFC.Endpoints.Employees;

public static class EmployeePost // metodo de criar
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(EmployeeRequest employeeRequest, HttpContext http, UserCreator userCreator)
    {
        // PEGA OS DADOS O NOME QUE ESTÁ NO TOKEN
        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        // AGRUPANDO AS CLAIM EM UMA LISTA
        var userClaims = new List<Claim>
        {
            new Claim("EmployeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name),
            new Claim("CreatedBy", userId), // PEGA PELA CLAIM QUEM FOI O USUÁRIO
        };

        // RESULTADO É UMA TULPLA (DOIS RETORNO)
        (IdentityResult identity, string userId) result = await userCreator.Create(employeeRequest.Email, employeeRequest.Password, userClaims);

        if (!result.identity.Succeeded)
            return Results.BadRequest(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/categories/{result.userId}", result.userId);
    }
}