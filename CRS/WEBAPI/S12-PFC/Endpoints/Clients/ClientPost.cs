using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using S12_PFC.Domain.Users;

using System.Security.Claims;

namespace S12_PFC.Endpoints.Clients;

public static class ClientPost // metodo de criar
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClientRequest clientRequest, UserCreator userCreator)
    {
        var userClaims = new List<Claim>
        {
            new Claim("Cpf", clientRequest.Cpf),
            new Claim("Name", clientRequest.Name)
        };

        // RESULTADO É UMA TULPLA (DOIS RETORNO)
        (IdentityResult identity, string userId) result = await userCreator.Create(clientRequest.Email, clientRequest.Password, userClaims);

        if (!result.identity.Succeeded)
            return Results.BadRequest(result.identity.Errors.ConvertToProblemDetails());

        return Results.Created($"/clients/{result.userId}", result.userId);
    }
}