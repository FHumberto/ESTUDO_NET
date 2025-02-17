using AutoMapper;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class CommentService
    (ICommentRepository commentRepository, IUserService userService, IServiceProvider serviceProvider, IMapper mapper) : ICommentService
{
    public async Task<Response<IReadOnlyList<QueryCommentResponseDto>>> GetAllComment()
    {
        var comments = await commentRepository.GetAllAsync();
        var response = mapper.Map<IReadOnlyList<QueryCommentResponseDto>>(comments);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryCommentResponseDto>>([], NotFound)
            : new Response<IReadOnlyList<QueryCommentResponseDto>>(response, Success);
    }

    public async Task<Response<QueryCommentResponseDto?>> GetCommentById(int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);
        var response = mapper.Map<QueryCommentResponseDto>(comment);

        return response == null
            ? new Response<QueryCommentResponseDto?>(null, NotFound, "Comentário não encontrado.")
            : new Response<QueryCommentResponseDto?>(response, Success);
    }

    public async Task<Response<int>> CreateComment(CreateCommentDto request)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            return new Response<int>(0, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        var userId = userService.UserId;

        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();

        #endregion

        #region ====[3.MAPEAMENTO]======================================================================================

        var commentToCreate = mapper.Map<Comment>(request);

        commentToCreate.UserId = userId;
        commentToCreate.CreatedBy = userId;

        #endregion

        #region ====[4.ACÃO]============================================================================================

        var createdCommentId = await commentRepository.CreateAsync(commentToCreate);

        return new Response<int>(createdCommentId);

        #endregion
    }

    public async Task<Response<bool>> UpdateComment(UpdateCommentDto request, int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var commentToUpdate = await commentRepository.GetByIdAsync(id);

        if (commentToUpdate is null)
        {
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        if (userService.UserId != commentToUpdate.UserId)
        {
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3.MAPEAMENTO]======================================================================================

        mapper.Map(request, commentToUpdate);

        commentToUpdate.UpdatedBy = userService.UserId;

        #endregion

        #region ====[4.ACÃO]============================================================================================

        await commentRepository.UpdateAsync(commentToUpdate);

        return new Response<bool>(true, Success, "Comentário atualizado.");

        #endregion
    }

    public async Task<Response<bool>> DeleteCommentById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var comment = await commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        #endregion

        #region ====[2.ACÃO]============================================================================================

        await commentRepository.DeleteAsync(comment);

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> SoftDeleteCommentById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        Comment? commentToDelete = await commentRepository.GetByIdAsync(id);

        if (commentToDelete is null)
        {
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        if (commentToDelete.UserId != userService.UserId)
        {
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[4.ACÃO]============================================================================================

        var operation = commentRepository.SoftDeleteAsync(commentToDelete);

        return operation.Result
            ? new Response<bool>(true, Success)
            : new Response<bool>(true, Error, "Erro durante a operação de deletar o comentário.");

        #endregion
    }
}