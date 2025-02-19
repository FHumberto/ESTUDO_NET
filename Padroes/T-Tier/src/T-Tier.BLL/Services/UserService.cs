using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        ILogger<UserService> logger,
        IMapper mapper) : IUserService
{

    public string? UserId { get => httpContextAccessor.HttpContext?.User?.FindFirstValue("uid"); }

    public async Task<Response<LoginResponseDto>> Login(LoginRequestDto request)
    {
        #region ====[1. VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando validação do login para {Email}.", request.Email);

        var validationErrors = await serviceProvider.ValidateAsync(request);
        if (validationErrors.Count != 0)
        {
            logger.LogWarning("BLL-SERV: Validação falhou para {Email}. Erros: {Errors}", request.Email, validationErrors);
            return new Response<LoginResponseDto>(null!, InvalidInput, validationErrors);
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário {Email} não encontrado.", request.Email);
            return new Response<LoginResponseDto>(null!, NotFound, "Usuário não encontrado.");
        }

        #endregion

        #region ====[2. AUTENTICAÇÃO]====================================================================================

        logger.LogInformation("BLL-SERV: Iniciando autenticação para {Email}.", request.Email);

        var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!passwordCheck.Succeeded)
        {
            logger.LogWarning("BLL-SERV: Falha na autenticação para {Email}. Credenciais inválidas.", request.Email);
            return new Response<LoginResponseDto>(null!, InvalidInput, "Credenciais inválidas.");
        }

        #endregion

        #region ====[3. GERAÇÃO DE TOKEN]================================================================================

        try
        {
            logger.LogInformation("BLL-SERV: Gerando token JWT para {Email}.", request.Email);

            var jwtSecurityToken = await GenerateToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            logger.LogInformation("BLL-SERV: Usuário {Email} autenticado com sucesso.", request.Email);

            return new Response<LoginResponseDto>(new LoginResponseDto
            {
                Token = token,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty
            }, Success);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao gerar token JWT para o usuário {Email}.", request.Email);
            return new Response<LoginResponseDto>(null!, Error, "Erro ao gerar token.");
        }

        #endregion
    }

    public async Task<Response<RegisterResponseDto>> Register(RegisterRequestDto request)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando validação do registro para o e-mail {Email}.", request.Email);

        var validationErrors = await serviceProvider.ValidateAsync(request);

        if (validationErrors.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Erros de validação encontrados para o e-mail {Email}.", request.Email);
            return new Response<RegisterResponseDto>(null!, InvalidInput, validationErrors);
        }

        var userExists = await userManager.FindByEmailAsync(request.Email);

        if (userExists is not null)
        {
            logger.LogWarning("BLL-SERV: Tentativa de registro com e-mail já existente: {Email}.", request.Email);
            return new Response<RegisterResponseDto>(null!, Conflict, "Já existe um usuário com o e-mail informado.");
        }

        #endregion

        #region ====[2. MAPEAMENTO]=====================================================================================

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName
        };

        logger.LogInformation("BLL-SERV: Usuário mapeado para registro: {Email}.", request.Email);

        #endregion

        #region ====[3.AÇÃO]============================================================================================

        try
        {
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                logger.LogWarning("BLL-SERV: Falha ao registrar o usuário {Email}. Erros: {Errors}",
                    request.Email, string.Join(", ", result.Errors.Select(e => e.Description)));

                return new Response<RegisterResponseDto>
                    (null!, InvalidInput, result.Errors.Select(e => e.Description).ToList());
            }

            logger.LogInformation("BLL-SERV: Usuário {Email} criado com sucesso.", request.Email);

            var roleExists = await roleManager.RoleExistsAsync("Default");

            if (!roleExists)
            {
                logger.LogInformation("BLL-SERV: Criando role 'Default'.");
                await roleManager.CreateAsync(new IdentityRole("Default"));
            }

            await userManager.AddToRoleAsync(user, "Default");
            logger.LogInformation("BLL-SERV: Usuário {Email} adicionado à role 'Default'.", request.Email);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
        };

            await userManager.AddClaimsAsync(user, claims);
            logger.LogInformation("BLL-SERV: Claims adicionadas ao usuário {Email}.", request.Email);

            return new Response<RegisterResponseDto>(new RegisterResponseDto { UserId = user.Id }, Success);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro inesperado ao registrar o usuário {Email}.", request.Email);
            return new Response<RegisterResponseDto>(null!, Error, "Erro ao registrar usuário.");
        }

        #endregion
    }

    public async Task<Response<QueryUserResponseDto>> FindUserById(string userId)
    {
        #region ====[1. BUSCA DO USUÁRIO]===================================================================================

        logger.LogInformation("BLL-SERV: Iniciando busca do usuário com ID {UserId}.", userId);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário com ID {UserId} não encontrado.", userId);
            return new Response<QueryUserResponseDto>(null!, NotFound, "Usuário não encontrado.");
        }

        #endregion

        #region ====[2. OBTENDO PERFIS]=====================================================================================

        var roles = await userManager.GetRolesAsync(user);

        #endregion

        #region ====[3. MAPEAMENTO]========================================================================================

        try
        {
            var response = mapper.Map<QueryUserResponseDto>(user);
            response.UserRole = roles.Any() ? string.Join(", ", roles) : "Sem Perfil Associado.";

            logger.LogInformation("BLL-SERV: Usuário com ID {UserId} encontrado e mapeado com sucesso.", userId);

            return new Response<QueryUserResponseDto>(response, Success);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear usuário com ID {UserId}.", userId);
            return new Response<QueryUserResponseDto>(null!, Error, "Erro ao processar os dados do usuário.");
        }

        #endregion
    }

    public async Task<Response<IReadOnlyList<QueryUserPostsResponseDto>>> FindPostsWithUser(string userId)
    {
        logger.LogInformation("BLL-SERV: Iniciando busca de posts para o usuário {UserId}.", userId);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário {UserId} não encontrado.", userId);
            return new Response<IReadOnlyList<QueryUserPostsResponseDto>>(null!, NotFound);
        }

        var posts = await postRepository.GetPostsWithUser(userId);

        try
        {
            var response = mapper.Map<IReadOnlyList<QueryUserPostsResponseDto>>(posts);

            if (response == null || response.Count == 0)
            {
                logger.LogInformation("BLL-SERV: Nenhum post encontrado para o usuário {UserId}.", userId);
                return new Response<IReadOnlyList<QueryUserPostsResponseDto>>([], NotFound);
            }

            logger.LogInformation("BLL-SERV: {Count} posts encontrados para o usuário {UserId}.", response.Count, userId);
            return new Response<IReadOnlyList<QueryUserPostsResponseDto>>(response, Success);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear posts do usuário {UserId}.", userId);
            return new Response<IReadOnlyList<QueryUserPostsResponseDto>>(null!, Error, "Erro ao processar os dados.");
        }
    }

    public async Task<Response<IReadOnlyList<QueryUserCommentsResponseDto>>> FindCommentsWithUser(string userId)
    {
        #region ====[1. BUSCA DE USUARIO]===================================================================================

        logger.LogInformation("BLL-SERV: Iniciando busca de comentários para o usuário {UserId}.", userId);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário {UserId} não encontrado.", userId);
            return new Response<IReadOnlyList<QueryUserCommentsResponseDto>>(null!, NotFound, "Usuário não encontrado.");
        }

        #endregion

        #region ====[2. OBTENÇÃO DE DADOS]==================================================================================

        var comments = await commentRepository.GetCommentsWithUser(userId);

        #endregion

        #region ====[3. MAPEAMENTO]=========================================================================================

        try
        {
            var response = mapper.Map<IReadOnlyList<QueryUserCommentsResponseDto>>(comments);

            if (response == null || response.Count == 0)
            {
                logger.LogInformation("BLL-SERV: Nenhum comentário encontrado para o usuário {UserId}.", userId);
                return new Response<IReadOnlyList<QueryUserCommentsResponseDto>>([], NotFound, "Não foram encontrados comentários.");
            }

            logger.LogInformation("BLL-SERV: {Count} comentários encontrados para o usuário {UserId}.", response.Count, userId);
            return new Response<IReadOnlyList<QueryUserCommentsResponseDto>>(response, Success);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear comentários do usuário {UserId}.", userId);
            return new Response<IReadOnlyList<QueryUserCommentsResponseDto>>(null!, Error, "Erro ao processar os dados.");
        }

        #endregion
    }

    public async Task<Response<IReadOnlyList<QueryUserRoleResponseDto>>> GetAllRoles()
    {
        #region ====[1. OBTENÇÃO DE DADOS]==================================================================================

        logger.LogInformation("BLL-SERV: Iniciando busca de todas as roles.");

        var roles = await roleManager.Roles.ToListAsync();

        #endregion

        #region ====[2. MAPEAMENTO]=========================================================================================

        try
        {
            var response = mapper.Map<IReadOnlyList<QueryUserRoleResponseDto>>(roles);

            if (response == null || response.Count == 0)
            {
                logger.LogInformation("BLL-SERV: Nenhuma role encontrada.");
                return new Response<IReadOnlyList<QueryUserRoleResponseDto>>(null!, NotFound, "Não foram encontrados perfis de usuário.");
            }

            logger.LogInformation("BLL-SERV: {Count} roles encontradas.", response.Count);
            return new Response<IReadOnlyList<QueryUserRoleResponseDto>>(response, Success);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear roles.");
            return new Response<IReadOnlyList<QueryUserRoleResponseDto>>(null!, Error, "Erro ao processar os dados.");
        }

        #endregion
    }

    public async Task<Response<bool>> UpdateUserRole(UpdateUserRoleRequestDto request, string userId)
    {
        #region ====[1. VALIDAÇÃO]==========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando atualização de role para o usuário {UserId}.", userId);

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário {UserId} não encontrado.", userId);
            return new Response<bool>(false, NotFound, "Usuário não encontrado.");
        }

        var validationErrors = await serviceProvider.ValidateAsync(request);

        if (validationErrors.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Erros de validação ao atualizar role do usuário {UserId}: {Errors}", userId, validationErrors);
            return new Response<bool>(false, InvalidInput, validationErrors);
        }

        var roleName = request.RoleName!.ToUpper();
        var roleExists = await roleManager.FindByNameAsync(roleName);

        if (roleExists is null)
        {
            logger.LogWarning("BLL-SERV: Perfil {RoleName} não encontrado.", roleName);
            return new Response<bool>(false, NotFound, "Perfil não encontrado.");
        }

        #endregion

        #region ====[2. AÇÃO]===============================================================================================

        logger.LogInformation("BLL-SERV: Removendo roles antigas do usuário {UserId}.", userId);

        var currentRoles = await userManager.GetRolesAsync(user);
        var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);

        if (!removeResult.Succeeded)
        {
            logger.LogError("BLL-SERV: Erro ao remover roles antigas do usuário {UserId}: {Errors}", userId, removeResult.Errors);
            return new Response<bool>(false, Error, "Falha ao remover roles anteriores.");
        }

        logger.LogInformation("BLL-SERV: Adicionando nova role {RoleName} para o usuário {UserId}.", roleName, userId);

        var addRoleResult = await userManager.AddToRoleAsync(user, roleName);

        if (!addRoleResult.Succeeded)
        {
            logger.LogError("BLL-SERV: Erro ao adicionar nova role {RoleName} para o usuário {UserId}: {Errors}", roleName, userId, addRoleResult.Errors);
            return new Response<bool>(false, Error, "Falha ao atribuir a nova role.");
        }

        logger.LogInformation("BLL-SERV: Role {RoleName} atualizada com sucesso para o usuário {UserId}.", roleName, userId);
        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> DeleteUser(string userId)
    {
        #region ====[1. VALIDAÇÃO]==========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando exclusão do usuário {UserId}.", userId);

        if (string.IsNullOrWhiteSpace(userId))
        {
            logger.LogWarning("BLL-SERV: Tentativa de exclusão com ID de usuário inválido.");
            return new Response<bool>(false, InvalidInput, "O ID de usuário é obrigatório.");
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário {UserId} não encontrado.", userId);
            return new Response<bool>(false, NotFound, "Usuário não encontrado.");
        }

        #endregion

        #region ====[2. AÇÃO]===============================================================================================

        logger.LogInformation("BLL-SERV: Removendo usuário {UserId} e suas dependências.", userId);

        bool success = await userRepository.DeleteUserWithDependenciesAsync(userId);

        if (!success)
        {
            logger.LogError("BLL-SERV: Erro ao excluir o usuário {UserId}.", userId);
            return new Response<bool>(false, Error, "Erro ao excluir o usuário.");
        }

        logger.LogInformation("BLL-SERV: Usuário {UserId} excluído com sucesso.", userId);
        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> SoftDeleteUser(string userId)
    {
        #region ====[1. VALIDAÇÃO]==========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando soft delete para o usuário {UserId}.", userId);

        if (string.IsNullOrWhiteSpace(userId))
        {
            logger.LogWarning("BLL-SERV: Tentativa de soft delete com ID de usuário inválido.");
            return new Response<bool>(false, InvalidInput, "O ID de usuário é obrigatório.");
        }

        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
        {
            logger.LogWarning("BLL-SERV: Usuário {UserId} não encontrado.", userId);
            return new Response<bool>(false, NotFound);
        }

        #endregion

        #region ====[2. AÇÃO]===============================================================================================

        logger.LogInformation("BLL-SERV: Marcando usuário {UserId} como deletado.", userId);

        var result = await userRepository.SoftDeleteAsync(user);

        if (!result)
        {
            logger.LogError("BLL-SERV: Erro ao tentar realizar o soft delete do usuário {UserId}.", userId);
            return new Response<bool>(false, Error, "Erro durante a operação de deletar o usuário.");
        }

        logger.LogInformation("BLL-SERV: Soft delete concluído para o usuário {UserId}.", userId);
        return new Response<bool>(true, Success);

        #endregion
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