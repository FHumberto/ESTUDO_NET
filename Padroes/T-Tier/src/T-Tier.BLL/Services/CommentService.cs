using AutoMapper;
using Microsoft.Extensions.Logging;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class CommentService
    (ICommentRepository commentRepository,
     IUserService userService,
     IServiceProvider serviceProvider,
     ILogger<CommentService> logger,
     IMapper mapper)
    : ICommentService
{
    public async Task<Response<IReadOnlyList<QueryCommentResponseDto>>> GetAllComment()
    {
        #region ====[1. BUSCAR COMENTÁRIOS]================================================================================

        var comments = await commentRepository.GetAllAsync();

        #endregion

        #region ====[2. MAPEAR E RETORNAR RESPOSTA]========================================================================

        try
        {
            var response = mapper.Map<IReadOnlyList<QueryCommentResponseDto>>(comments);

            if (response == null || response.Count == 0)
            {
                logger.LogWarning("BLL-SERV: Nenhum comentário encontrado.");
                return new Response<IReadOnlyList<QueryCommentResponseDto>>([], NotFound);
            }

            return new Response<IReadOnlyList<QueryCommentResponseDto>>(response, Success);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear comentários para QueryCommentResponseDto.");
            return new Response<IReadOnlyList<QueryCommentResponseDto>>([], Error, "Erro ao processar os comentários.");
        }

        #endregion
    }

    public async Task<Response<QueryCommentResponseDto?>> GetCommentById(int id)
    {
        #region ====[1. BUSCAR COMENTÁRIO]=================================================================================

        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            logger.LogWarning("BLL-SERV: Comentário com ID {CommentId} não encontrado.", id);
            return new Response<QueryCommentResponseDto?>(null, NotFound, "Comentário não encontrado.");
        }

        #endregion

        #region ====[2. MAPEAR E RETORNAR RESPOSTA]========================================================================

        try
        {
            var response = mapper.Map<QueryCommentResponseDto>(comment);
            return new Response<QueryCommentResponseDto?>(response, Success);
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear comentário ID {CommentId} para QueryCommentResponseDto.", id);
            return new Response<QueryCommentResponseDto?>(null, Error, "Erro ao processar o comentário.");
        }

        #endregion
    }

    public async Task<Response<int>> CreateComment(CreateCommentDto request)
    {
        #region ====[1. VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando validação do comentário.");

        var validationResult = await serviceProvider.ValidateAsync(request);
        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Validação falhou para o comentário. Erros: {ValidationErrors}", validationResult);
            return new Response<int>(0, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2. REGRA]===========================================================================================

        logger.LogInformation("BLL-SERV: Obtendo ID do usuário autenticado.");

        var userId = userService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            logger.LogError("BLL-SERV: Usuário não autenticado. Acesso não autorizado.");
            throw new UnauthorizedAccessException();
        }

        #endregion

        #region ====[3. MAPEAMENTO]======================================================================================

        logger.LogInformation("BLL-SERV: Mapeando dados de CreateCommentDto para entidade Comment.");

        var commentToCreate = mapper.Map<Comment>(request);
        commentToCreate.UserId = userId;
        commentToCreate.CreatedBy = userId;

        #endregion

        #region ====[4. AÇÃO]============================================================================================

        logger.LogInformation("BLL-SERV: Criando novo comentário para o usuário {UserId}.", userId);

        var createdCommentId = await commentRepository.CreateAsync(commentToCreate);

        if (createdCommentId == 0)
        {
            return new Response<int>(0, Error);
        }

        logger.LogInformation("BLL-SERV: Comentário criado com sucesso. ID: {CreatedCommentId}.", createdCommentId);

        return new Response<int>(createdCommentId);

        #endregion
    }

    public async Task<Response<bool>> UpdateComment(UpdateCommentDto request, int id)
    {
        #region ====[1. VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando atualização do comentário com ID {CommentId}.", id);

        var commentToUpdate = await commentRepository.GetByIdAsync(id);
        if (commentToUpdate is null)
        {
            logger.LogWarning("BLL-SERV: Comentário com ID {CommentId} não encontrado.", id);
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);
        if (validationResult.Count > 0)
        {
            logger.LogWarning("BLL-SERV: Erros de validação ao atualizar comentário com ID {CommentId}. Erros: {ValidationErrors}", id, validationResult);
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        logger.LogInformation("BLL-SERV: Validação concluída para o comentário com ID {CommentId}.", id);

        #endregion

        #region ====[2. REGRA]===========================================================================================

        logger.LogInformation("BLL-SERV: Verificando autorização para atualizar o comentário com ID {CommentId}.", id);

        if (userService.UserId != commentToUpdate.UserId)
        {
            logger.LogError("BLL-SERV: Usuário {UserId} não autorizado para atualizar o comentário com ID {CommentId}.", userService.UserId, id);
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3. MAPEAMENTO]======================================================================================

        logger.LogInformation("BLL-SERV: Mapeando dados de UpdateCommentDto para o comentário com ID {CommentId}.", id);

        try
        {
            mapper.Map(request, commentToUpdate);
            commentToUpdate.UpdatedBy = userService.UserId;
        }
        catch (AutoMapperMappingException ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao mapear dados para o comentário com ID {CommentId}.", id);
            return new Response<bool>(false, Error, "Erro ao processar os dados do comentário.");
        }

        #endregion

        #region ====[4. AÇÃO]============================================================================================

        logger.LogInformation("BLL-SERV: Atualizando comentário com ID {CommentId}.", id);

        var operation = await commentRepository.UpdateAsync(commentToUpdate);

        if (!operation)
        {
            return new Response<bool>(false, Error);
        }

        logger.LogInformation("BLL-SERV: Comentário com ID {CommentId} atualizado com sucesso.", id);

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> DeleteCommentById(int id)
    {
        #region ====[1. VALIDAÇÃO]=======================================================================================

        logger.LogInformation("BLL-SERV: Iniciando exclusão do comentário com ID {CommentId}.", id);

        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            logger.LogWarning("BLL-SERV: Comentário com ID {CommentId} não encontrado.", id);
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        #endregion

        #region ====[2. AÇÃO]============================================================================================

        try
        {
            logger.LogInformation("BLL-SERV: Excluindo comentário com ID {CommentId}.", id);

            await commentRepository.DeleteAsync(comment);

            logger.LogInformation("BLL-SERV: Comentário com ID {CommentId} excluído com sucesso.", id);

            return new Response<bool>(true, Success);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BLL-SERV: Erro ao excluir comentário com ID {CommentId}.", id);
            return new Response<bool>(false, Error, "Erro ao excluir comentário.");
        }

        #endregion
    }

    public async Task<Response<bool>> SoftDeleteCommentById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var commentToDelete = await commentRepository.GetByIdAsync(id);

        if (commentToDelete is null)
        {
            logger.LogWarning("BLL-SERV: Comentário {CommentId} não encontrado.", id);
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        if (commentToDelete.UserId != userService.UserId)
        {
            logger.LogError("BLL-SERV: Usuário {UserId} não autorizado para soft delete do comentário {CommentId}.", userService.UserId, id);
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3.ACÃO]============================================================================================

        try
        {
            bool result = await commentRepository.SoftDeleteAsync(commentToDelete);

            if (!result)
            {
                logger.LogError("BLL-SERV: Falha ao executar soft delete do comentário {CommentId}.", id);

                return new Response<bool>(false, Error, "Erro durante a operação de deletar o comentário.");
            }

            logger.LogInformation("BLL-SERV: Soft delete do comentário {CommentId} realizado com sucesso.", id);

            return new Response<bool>(true, Success);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "BLL-SERV: Exceção ao executar soft delete do comentário {CommentId}.", id);
            return new Response<bool>(false, Error, "Erro durante a operação de deletar o comentário.");
        }

        #endregion
    }
}