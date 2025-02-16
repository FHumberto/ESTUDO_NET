using AutoMapper;
using FluentValidation.Results;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Validators.Comment;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class CommentService(ICommentRepository commentRepository, IMapper mapper) : ICommentService
{
    public async Task<Response<IReadOnlyList<QueryCommentResponseDto>>> GetAllComment()
    {
        var comments = await commentRepository.GetAllAsync();
        var response = mapper.Map<IReadOnlyList<QueryCommentResponseDto>>(comments);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryCommentResponseDto>>([], NotFound, "Comentários não encontrados.")
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
        //TODO: Recuperar pelo Token o ID do usuário que está cadastrando o token.
        CommentValidator validationRules = new(commentRepository);
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new Response<int>(0, InvalidInput, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        Comment commentToCreate = mapper.Map<Comment>(request);
        int createdCommentId = await commentRepository.CreateAsync(commentToCreate);

        return new Response<int>(createdCommentId, Success);
    }

    public async Task<Response<bool>> UpdateComment(UpdateCommentDto request, int id)
    {
        Comment? commentToUpdate = await commentRepository.GetByIdAsync(id);

        if (commentToUpdate == null)
        {
            return new Response<bool>(false, NotFound);
        }

        //TODO: Implementar regra para validar se o usuário for diferente do usuário que cadastrou
        //TODO: Recuperar pelo Token o ID do usuário que está atualizando o token.
        CommentUpdateValidator validationRules = new();
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new Response<bool>(false, InvalidInput, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        mapper.Map(request, commentToUpdate);

        await commentRepository.UpdateAsync(commentToUpdate);

        return new Response<bool>(true, Success);
    }

    public async Task<Response<bool>> DeleteComment(int id)
    {
        Comment? comment = await commentRepository.GetByIdAsync(id);

        if (comment == null)
        {
            return new Response<bool>(false, NotFound);
        }

        await commentRepository.DeleteAsync(comment);

        return new Response<bool>(true, Success);
    }

    public Task<Response<bool>> SoftDeleteComment(int id)
    {
        throw new NotImplementedException();
    }
}