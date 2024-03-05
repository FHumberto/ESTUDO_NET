using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.BlazorUI.Providers;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal user = new(new ClaimsIdentity());
        bool isTokenPresent = await _localStorage.ContainKeyAsync("token");

        if (!isTokenPresent)
        {
            //? returna o usuario sem claims anexado (similar ao nobody)
            return new AuthenticationState(user);
        }

        string? savedToken = await _localStorage.GetItemAsync<string>("token");
        JwtSecurityToken tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);

        //! se atentar com o datetime e utc
        if (tokenContent.ValidTo < DateTime.Now)
        {
            await _localStorage.RemoveItemAsync("token");
            return new AuthenticationState(user);
        }

        //? se chegou até aqui o token existe e ainda é válido
        List<Claim> claims = await GetClaims();

        user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }

    public async Task LoggedIn()
    {
        List<Claim> claims = await GetClaims();
        ClaimsPrincipal user = new(new ClaimsIdentity(claims, "jwt"));
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(user));

        //? func do blazor que notifica para all que o state mudou
        NotifyAuthenticationStateChanged(authState);
    }

    public async Task LoggedOut()
    {
        await _localStorage.RemoveItemAsync("token");
        ClaimsPrincipal nobody = new(new ClaimsIdentity());
        Task<AuthenticationState> authState = Task.FromResult(new AuthenticationState(nobody));

        //? func do blazor que notifica para all que o state mudou
        NotifyAuthenticationStateChanged(authState);
    }

    private async Task<List<Claim>> GetClaims()
    {
        string? savedToken = await _localStorage.GetItemAsync<string>("token");
        JwtSecurityToken tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(savedToken);
        List<Claim> claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));

        return claims;
    }
}
