using AutoMapper;
using Microsoft.Extensions.Logging;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class PostService
    (IPostRepository postRepository,
     IUserService userService,
     IServiceProvider serviceProvider,
     ILogger<PostService> logger,
     IMapper mapper)
    : IPostService
{
    public async Task<Response<IReadOnlyList<QueryPostResponseDto>>> GetAllPost()
    {
        #region ====[1. OBTENÇÃO DOS DADOS]================================================================================

        logger.LogInformation("BLL-SERV: Iniciando recuperação de todos os posts.");

        var posts = await postRepository.GetAllAsync();

        #endregion

        #region ====[2. MAPEAMENTO]========================================================================================

        IReadOnlyList<QueryPostResponseDto> response;

        try
        {
            response = mapper.Map<IReadOnlyList<QueryPostResponseDto>>(posts);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro de mapeamento no AutoMapper. Verifique os perfis de mapeamento.");
            return new Response<IReadOnlyList<QueryPostResponseDto>>([], Error, "Erro ao processar os posts.");
        }

        #endregion

        #region ====[3. RETORNO]===========================================================================================

        if (response == null || response.Count == 0)
        {
            logger.LogWarning("BLL-SERV: Nenhum post encontrado.");
            return new Response<IReadOnlyList<QueryPostResponseDto>>([], NotFound);
        }

        logger.LogInformation("BLL-SERV: {Count} posts recuperados com sucesso.", response.Count);
        return new Response<IReadOnlyList<QueryPostResponseDto>>(response, Success);

        #endregion
    }

    public async Task<Response<QueryPostResponseDto?>> GetPostById(int id)
    {
        #region ====[1. OBTENÇÃO DOS DADOS]================================================================================

        logger.LogInformation("BLL-SERV: Iniciando recuperação da postagem com ID {PostId}.", id);

        Post? post = await postRepository.GetByIdAsync(id);

        if (post == null)
        {
            logger.LogWarning("BLL-SERV: Postagem com ID {PostId} não encontrada.", id);
            return new Response<QueryPostResponseDto?>(null, NotFound);
        }

        #endregion

        #region ====[2. MAPEAMENTO]========================================================================================

        QueryPostResponseDto? response;

        try
        {
            response = mapper.Map<QueryPostResponseDto>(post);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro de mapeamento no AutoMapper. Verifique os perfis de mapeamento.");
            return new Response<QueryPostResponseDto?>(null, Error, "Erro ao processar a postagem.");
        }

        #endregion

        #region ====[3. RETORNO]===========================================================================================

        logger.LogInformation("BLL: Postagem com ID {PostId} recuperada com sucesso.", id);
        return new Response<QueryPostResponseDto?>(response, Success);

        #endregion
    }

    public async Task<Response<QueryPostTagResponseDto?>> GetPostByIdWithTag(int id)
    {
        #region ====[1. OBTENÇÃO DOS DADOS]================================================================================

        logger.LogInformation("BLL-SERV: Iniciando recuperação da postagem com tags para ID {PostId}.", id);

        Post? post = await postRepository.GetPostByIdWithTagsAsync(id);

        if (post == null)
        {
            logger.LogWarning("BLL-SERV: Postagem com ID {PostId} não encontrada.", id);
            return new Response<QueryPostTagResponseDto?>(null, NotFound);
        }

        #endregion

        #region ====[2. MAPEAMENTO]========================================================================================

        QueryPostTagResponseDto? response;

        try
        {
            response = mapper.Map<QueryPostTagResponseDto>(post);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro de mapeamento AutoMapper ao converter postagem com tags para ID {PostId}.", id);
            return new Response<QueryPostTagResponseDto?>(null, Error, "Erro ao processar a postagem.");
        }

        #endregion

        #region ====[3. RETORNO]===========================================================================================

        logger.LogInformation("BLL-SERV: Postagem com ID {PostId} e suas tags recuperadas com sucesso.", id);
        return new Response<QueryPostTagResponseDto?>(response, Success);

        #endregion
    }

    public async Task<Response<QueryPostCommentsResponseDto?>> GetPostByIdWithComments(int id)
    {
        #region ====[1. OBTENÇÃO DOS DADOS]================================================================================

        logger.LogInformation("BLL-SERV: Iniciando recuperação da postagem com comentários para ID {PostId}.", id);

        Post? post = await postRepository.GetPostByidWithCommentsAsync(id);

        if (post == null)
        {
            logger.LogWarning("BLL-SERV: Postagem com ID {PostId} não encontrada.", id);
            return new Response<QueryPostCommentsResponseDto?>(null, NotFound);
        }

        #endregion

        #region ====[2. MAPEAMENTO]========================================================================================

        QueryPostCommentsResponseDto? response;

        try
        {
            response = mapper.Map<QueryPostCommentsResponseDto>(post);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro de mapeamento AutoMapper ao converter postagem com comentários para ID {PostId}.", id);
            return new Response<QueryPostCommentsResponseDto?>(null, Error, "Erro ao processar a postagem.");
        }

        #endregion

        #region ====[3. RETORNO]===========================================================================================

        logger.LogInformation("BLL-SERV: Postagem com ID {PostId} e seus comentários recuperados com sucesso.", id);
        return new Response<QueryPostCommentsResponseDto?>(response, Success);

        #endregion
    }

    public async Task<Response<int>> CreatePost(CommandPostRequestDto request)
    {
        #region ====[1. VALIDAÇÃO]=========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando criação de postagem.");

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Validação falhou ao criar postagem.");
            return new Response<int>(0, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2. REGRA]=============================================================================================

        var userId = userService.UserId;

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogError("BLL-SERV: Tentativa de criação de postagem sem usuário autenticado.");
            throw new UnauthorizedAccessException();
        }

        #endregion

        #region ====[3. MAPEAMENTO]========================================================================================

        Post? postToCreate;

        try
        {
            postToCreate = mapper.Map<Post>(request);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro de mapeamento AutoMapper ao criar postagem.");
            return new Response<int>(0, Error, "Erro ao processar a postagem.");
        }

        postToCreate.UserId = userId;
        postToCreate.CreatedBy = userId;

        #endregion

        #region ====[4. AÇÃO]==============================================================================================

        var createdPostId = await postRepository.CreateAsync(postToCreate);

        if (createdPostId == 0)
        {
            return new Response<int>(0, Error);
        }

        logger.LogInformation("BLL-SERV: Postagem criada com sucesso. ID: {PostId}", createdPostId);

        return new Response<int>(createdPostId, Success);

        #endregion
    }

    public async Task<Response<bool>> UpdatePost(CommandPostRequestDto request, int id)
    {
        #region ====[1. VALIDAÇÃO]=========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando atualização da postagem. ID: {PostId}", id);

        var postToUpdate = await postRepository.GetByIdAsync(id);

        if (postToUpdate is null)
        {
            logger.LogWarning("BLL-SERV: Postagem não encontrada. ID: {PostId}", id);
            return new Response<bool>(false, NotFound, "Postagem não encontrada.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Validação falhou para a postagem ID: {PostId}", id);
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2. REGRA]=============================================================================================

        if (userService.UserId != postToUpdate.UserId)
        {
            logger.LogError("BLL-SERV: Tentativa de atualização de postagem não autorizada. ID: {PostId}", id);
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3. MAPEAMENTO]========================================================================================

        try
        {
            mapper.Map(request, postToUpdate);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro de mapeamento AutoMapper ao atualizar postagem. ID: {PostId}", id);
            return new Response<bool>(false, Error, "Erro ao processar a atualização da postagem.");
        }

        postToUpdate.UpdatedBy = userService.UserId;

        #endregion

        #region ====[4. AÇÃO]==============================================================================================

        var operation = await postRepository.UpdateAsync(postToUpdate);

        if (!operation)
        {
            return new Response<bool>(false, Error);
        }

        logger.LogInformation("BLL-SERV: Postagem atualizada com sucesso. ID: {PostId}", id);
        return new Response<bool>(true, Success);

        #endregion
    }
    public async Task<Response<bool>> DeletePostById(int id)
    {
        #region ====[1. VALIDAÇÃO]========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando exclusão da postagem. ID: {PostId}", id);

        var post = await postRepository.GetByIdAsync(id);

        if (post is null)
        {
            logger.LogWarning("BLL-SERV: Postagem não encontrada para exclusão. ID: {PostId}", id);
            return new Response<bool>(false, NotFound, "Postagem não encontrada.");
        }

        #endregion

        #region ====[2. AÇÃO]============================================================================================

        var operation = await postRepository.DeleteAsync(post);

        if (!operation)
        {
            logger.LogError("BLL-SERV: Falha ao excluir a postagem. ID: {PostId}", id);
            return new Response<bool>(false, Error, "Erro ao excluir a postagem.");
        }

        logger.LogInformation("BLL-SERV: Postagem excluída com sucesso. ID: {PostId}", id);
        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> SoftDeletePostById(int id)
    {
        #region ====[1. VALIDAÇÃO]========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando soft delete da postagem. ID: {PostId}", id);

        var postToDelete = await postRepository.GetByIdAsync(id);

        if (postToDelete is null)
        {
            logger.LogWarning("BLL-SERV: Postagem não encontrada para exclusão lógica. ID: {PostId}", id);
            return new Response<bool>(false, NotFound, "Postagem não encontrada.");
        }

        #endregion

        #region ====[2. REGRA]============================================================================================

        if (postToDelete.UserId != userService.UserId)
        {
            logger.LogError("BLL-SERV: Tentativa de exclusão lógica não autorizada. ID: {PostId}", id);
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3. AÇÃO]============================================================================================

        var operation = await postRepository.SoftDeleteAsync(postToDelete);

        if (!operation)
        {
            logger.LogError("BLL-SERV: Falha ao realizar soft delete da postagem. ID: {PostId}", id);
            return new Response<bool>(false, Error, "Erro durante a operação de exclusão lógica da postagem.");
        }

        logger.LogInformation("BLL-SERV: Postagem excluída logicamente com sucesso. ID: {PostId}", id);
        return new Response<bool>(true, Success);

        #endregion
    }
}