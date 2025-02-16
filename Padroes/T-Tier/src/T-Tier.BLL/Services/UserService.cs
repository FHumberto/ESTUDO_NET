using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using T_Tier.BLL.DTOs.Users;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Settings;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<User> signInManager,
        JwtSettings jwtSettings,
        IHttpContextAccessor httpContextAccessor,
        IServiceProvider serviceProvider,
        IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IMapper mapper) : IUserService
{

    public string? UserId { get => httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

    public async Task<Response<LoginResponseDto>> Login(LoginRequestDto request)
    {
        var validationErrors = await serviceProvider.ValidateAsync(request);
        if (validationErrors.Count != 0)
            return new Response<LoginResponseDto>(null!, InvalidInput, validationErrors);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return new Response<LoginResponseDto>(null!, NotFound, "Usuário não encontrado.");

        var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!passwordCheck.Succeeded)
            return new Response<LoginResponseDto>(null!, InvalidInput, "Credenciais inválidas.");

        var jwtSecurityToken = await GenerateToken(user);

        return new Response<LoginResponseDto>(new LoginResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty
        }, Success);
    }

    public async Task<Response<RegisterResponseDto>> Register(RegisterRequestDto request)
    {
        var validationErrors = await serviceProvider.ValidateAsync(request);
        if (validationErrors.Count > 0)
            return new Response<RegisterResponseDto>(null!, InvalidInput, validationErrors);

        var userExists = await userManager.FindByEmailAsync(request.Email);

        if (userExists is not null)
            return new Response<RegisterResponseDto>
                (null!, Conflict, "Já exite um usuário com o e-mail informado.");

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return new Response<RegisterResponseDto>
                (null!, InvalidInput, result.Errors.Select(e => e.Description).ToList());
        }

        await userManager.AddToRoleAsync(user, "Default");

        return new Response<RegisterResponseDto>(new RegisterResponseDto { UserId = user.Id }, Success);
    }

    public async Task<Response<QueryUserResponseDto>> FindUserById(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return new Response<QueryUserResponseDto>(null!, NotFound, "Usuário não encontrado.");
        }

        var roles = await userManager.GetRolesAsync(user);

        var response = mapper.Map<QueryUserResponseDto>(user);

        response.UserRole = roles.Any() ? string.Join(", ", roles) : "Sem Perfil Associado.";

        return response == null
            ? new Response<QueryUserResponseDto>(null!, NotFound, "Usuário não encontrado.")
            : new Response<QueryUserResponseDto>(response, Success);
    }

    public async Task<Response<IReadOnlyList<QueryUserPostsResponseDto>>> FindPostsWithUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return new Response<IReadOnlyList<QueryUserPostsResponseDto>>(null!, NotFound);

        var posts = await postRepository.GetPostsWithUser(userId);
        var response = mapper.Map<IReadOnlyList<QueryUserPostsResponseDto>>(posts);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryUserPostsResponseDto>>([], NotFound)
            : new Response<IReadOnlyList<QueryUserPostsResponseDto>>(response, Success);
    }

    public async Task<Response<IReadOnlyList<QueryUserCommentsResponseDto>>> FindCommentsWithUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return new Response<IReadOnlyList<QueryUserCommentsResponseDto>>(null!, NotFound, "Usuário não encontrado.");

        var comments = await commentRepository.GetCommentsWithUser(userId);
        var response = mapper.Map<IReadOnlyList<QueryUserCommentsResponseDto>>(comments);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryUserCommentsResponseDto>>([], NotFound, "Não foram encontradas postagens.")
            : new Response<IReadOnlyList<QueryUserCommentsResponseDto>>(response, Success);
    }

    public async Task<Response<IReadOnlyList<QueryUserRoleResponseDto>>> GetAllRoles()
    {
        var roles = await roleManager.Roles.ToListAsync();

        var response = mapper.Map<IReadOnlyList<QueryUserRoleResponseDto>>(roles);

        return response == null
            ? new Response<IReadOnlyList<QueryUserRoleResponseDto>>(null!, NotFound, "Não foram encontrados perfis de usuário")
            : new Response<IReadOnlyList<QueryUserRoleResponseDto>>(response, Success);
    }

    public async Task<Response<bool>> DeleteUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new Response<bool>(false, InvalidInput, "O ID de usuário é obrigatório.");

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return new Response<bool>(false, NotFound, "Usuário não encontrado");

        var result = await userManager.DeleteAsync(user);

        return result.Succeeded
            ? new Response<bool>(true, Success)
            : new Response<bool>(true, Error, "Erro durante a operação de deletar o usuário.");
    }

    public async Task<Response<bool>> SoftDeleteUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new Response<bool>(false, InvalidInput, "O ID de usuário é obrigatório.");

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            return new Response<bool>(false, NotFound);

        var result = await userRepository.SoftDeleteAsync(user);

        return result
            ? new Response<bool>(true, Success)
            : new Response<bool>(true, Error, "Erro durante a operação de deletar o usuário.");
    }

    private async Task<JwtSecurityToken> GenerateToken(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!), new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(jwtSettings.Key));
        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new
        (
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }

}