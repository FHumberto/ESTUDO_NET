using AutoMapper;
using Microsoft.Extensions.Logging;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class TagService(
    ITagRepository tagRepository,
    IPostRepository postRepository,
    IServiceProvider serviceProvider, ILogger<TagService> logger, IMapper mapper) : ITagService
{
    public async Task<Response<IReadOnlyList<QueryTagResponseDto>>> GetAllTag()
    {
        #region ====[1. BUSCA]===========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando busca de todas as tags.");

        var tags = await tagRepository.GetAllAsync();

        #endregion

        #region ====[2. MAPEAMENTO]======================================================================================

        IReadOnlyList<QueryTagResponseDto> response;

        try
        {
            response = mapper.Map<IReadOnlyList<QueryTagResponseDto>>(tags);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear a entidade Tag para QueryTagResponseDto.");
            return new Response<IReadOnlyList<QueryTagResponseDto>>([], Error, "Erro ao processar os dados das tags.");
        }

        #endregion

        #region ====[3. RETORNO]=========================================================================================

        if (response is null || response.Count == 0)
        {
            logger.LogWarning("BLL-SERV: Nenhuma tag encontrada.");
            return new Response<IReadOnlyList<QueryTagResponseDto>>([], NotFound);
        }

        logger.LogInformation("BLL-SERV: {Count} tags encontradas com sucesso.", response.Count);
        return new Response<IReadOnlyList<QueryTagResponseDto>>(response, Success);

        #endregion
    }

    public async Task<Response<QueryTagResponseDto?>> GetTagById(int id)
    {
        #region ====[1. BUSCA]===========================================================================================

        logger.LogInformation("BLL-SERV: Iniciando busca da tag com ID: {TagId}", id);

        var tag = await tagRepository.GetByIdAsync(id);

        if (tag is null)
        {
            logger.LogWarning("BLL-SERV: Tag com ID {TagId} não encontrada.", id);
            return new Response<QueryTagResponseDto?>(null, NotFound);
        }

        #endregion

        #region ====[2. MAPEAMENTO]======================================================================================

        QueryTagResponseDto? response;

        try
        {
            response = mapper.Map<QueryTagResponseDto>(tag);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear a entidade Tag para QueryTagResponseDto. ID: {TagId}", id);
            return new Response<QueryTagResponseDto?>(null, Error, "Erro ao processar os dados da tag.");
        }

        #endregion

        #region ====[3. RETORNO]=========================================================================================

        logger.LogInformation("BLL-SERV: Tag com ID {TagId} encontrada com sucesso.", id);
        return new Response<QueryTagResponseDto?>(response, Success);

        #endregion
    }

    public async Task<Response<int>> CreateTag(CommandTagDto request)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando validação para criação de tag. Nome: {TagName}", request.Name);

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Falha na validação ao criar tag. Erros: {@ValidationErrors}", validationResult);
            return new Response<int>(0, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.MAPEAMENTO]======================================================================================

        Tag tagToCreate;

        try
        {
            tagToCreate = mapper.Map<Tag>(request);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear CommandTagDto para Tag. Nome: {TagName}", request.Name);
            return new Response<int>(0, Error, "Erro ao processar os dados da tag.");
        }

        #endregion

        #region ====[3.ACÃO]============================================================================================

        logger.LogInformation("BLL-SERV: Tentando criar tag no banco de dados. Nome: {TagName}", request.Name);

        int createdTagId = await tagRepository.CreateAsync(tagToCreate);

        if (createdTagId == 0)
        {
            return new Response<int>(0, Error);
        }

        logger.LogInformation("BLL-SERV: Tag criada com sucesso. ID: {createdTagId}", createdTagId);

        return new Response<int>(createdTagId, ResponseTypeEnum.Success);

        #endregion
    }

    public async Task<Response<bool>> AddTagToPost(CommandAddTagPostRequest request, int postId)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando validação para adição das Tags ao Post: {PostId}", postId);

        Post? post = await postRepository.GetByIdAsync(postId);

        if (post is null)
        {
            logger.LogWarning("BLL-SERV: Post com ID {PostId} não encontrado.", postId);
            return new Response<bool>(false, NotFound, "Post não encontrado.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Falha na validação ao adicionar tags. Erros: {@ValidationErrors}", validationResult);
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        // Buscar as tags no banco
        var tags = await tagRepository.GetByIdsAsync(request.TagIds);
        if (tags.Count != request.TagIds.Count)
        {
            return new Response<bool>(false, InvalidInput, "Uma ou mais tags não existem.");
        }

        #endregion

        #region ====[2. ADIÇÃO DAS TAGS AO POST]=======================================================================

        foreach (var tag in tags)
        {
            if (post.Tags!.Any(t => t.Id == tag.Id))
            {
                post.Tags!.Add(tag);
            }
        }

        var opreation = await postRepository.UpdateAsync(post);

        if (!opreation)
        {
            return new Response<bool>(false, Error);
        }

        logger.LogInformation("BLL-SERV: Tags adicionadas ao post com sucesso. PostId: {PostId}, Tags: {TagIds}", postId, string.Join(",", request.TagIds));

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> UpdateTag(CommandTagDto request, int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando atualização da tag. ID: {TagId}", id);

        var tagToUpdate = await tagRepository.GetByIdAsync(id);

        if (tagToUpdate is null)
        {
            logger.LogWarning("BLL-SERV: Tag não encontrada para atualização. ID: {TagId}", id);
            return new Response<bool>(false, NotFound, "Tag não encontrada.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Falha na validação ao atualizar tag. ID: {TagId}, Erros: {@ValidationErrors}", id, validationResult);
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.MAPEAMENTO]======================================================================================

        try
        {
            mapper.Map(request, tagToUpdate);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear CommandTagDto para Tag. ID: {TagId}", id);
            return new Response<bool>(false, Error, "Erro ao processar os dados da tag.");
        }

        #endregion

        #region ====[3.ACÃO]============================================================================================

        var operation = await tagRepository.UpdateAsync(tagToUpdate);

        if (!operation)
        {
            return new Response<bool>(false, Error);
        }

        logger.LogInformation("BLL-SERV: Tag com ID {tagId} atualizada com sucesso.", id);

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> DeleteTagById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        logger.LogInformation("Iniciando exclusão da tag com ID {TagId}", id);

        var tagToDelete = await tagRepository.GetByIdAsync(id);

        if (tagToDelete is null)
        {
            logger.LogWarning("Tentativa de exclusão falhou: Tag com ID {TagId} não encontrada.", id);
            return new Response<bool>(false, NotFound, "Tag não encontrada.");
        }

        #endregion

        #region ====[2.ACÃO]============================================================================================

        var operation = await tagRepository.DeleteAsync(tagToDelete);

        if (!operation)
        {
            logger.LogError("Erro ao excluir a tag com ID {TagId}", id);
            return new Response<bool>(false, Error, "Erro ao excluir a tag.");
        }

        logger.LogInformation("Tag com ID {TagId} excluída com sucesso.", id);
        return new Response<bool>(true, Success);

        #endregion
    }
}
