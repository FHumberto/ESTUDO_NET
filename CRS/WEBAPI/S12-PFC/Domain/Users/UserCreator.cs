using Microsoft.AspNetCore.Identity;

using System.Security.Claims;

namespace S12_PFC.Domain.Users;

// CLASSE DE SERVIÇO (LEMBRAR DE ADDSCOPED)
public class UserCreator
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserCreator(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // RETORNAR UMA TULPLA <(VAL1, VAL2)> e tem dois retorno
    public async Task<(IdentityResult, string)> Create(string email, string password, List<Claim> claims)
    {
        var newUser = new IdentityUser { UserName = email, Email = email };
        var result = await _userManager.CreateAsync(newUser, password);

        if (!result.Succeeded)
            return (result, String.Empty);

        return (await _userManager.AddClaimsAsync(newUser, claims), newUser.Id);
    }
}
