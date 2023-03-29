using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

namespace S12_PFC.Endpoints.Clients;

public static class ClientGet
{
    public static string Template => "/clients";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // OBTEM OS DADOS DO USUARIO LOGADO ATRAVÉS DO TOKEN
    [AllowAnonymous]
    public static IResult Action(HttpContext http)
    {
        ClaimsPrincipal user = http.User;
        var result = new
        {
            Id = user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Name = user.Claims.First(c => c.Type == "Name").Value,
            Cpf = user.Claims.First(c => c.Type == "Cpf").Value,
        };

        return Results.Ok(result);
    }
}