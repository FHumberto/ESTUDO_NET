using AutoMapper;
using FluentValidation.Results;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.BLL.Validators;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class CommentService(ICommentRepository commentRepository, IMapper mapper)
{
    public async Task<Response<IReadOnlyList<QueryCommentDto>>> GetAllCommentAsync()
    {
        IReadOnlyList<Comment> comments = await commentRepository.GetAllAsync();
        IReadOnlyList<QueryCommentDto> response = mapper.Map<IReadOnlyList<QueryCommentDto>>(comments);
    
        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryCommentDto>>(new List<QueryCommentDto>(), NotFound)
            : new Response<IReadOnlyList<QueryCommentDto>>(response, Success);
    }
    
    public async Task<Response<QueryCommentDto?>> GetCommentByIdAsync(int id)
    {
        Comment? comment = await commentRepository.GetByIdAsync(id);
        QueryCommentDto? response = mapper.Map<QueryCommentDto>(comment);
    
        return response == null
            ? new Response<QueryCommentDto?>(null, NotFound)
            : new Response<QueryCommentDto?>(response, Success);
    }

    public async Task<Response<int>> CreateCommentAsync(CommandCommentDto request)
    {
        CommentValidator validationRules = new();
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);
    
        if (!validationResult.IsValid)
        {
            return new Response<int>(0, InvalidInput, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
    
        Comment commentToCreate = mapper.Map<Comment>(request);
        int createdCommentId = await commentRepository.CreateAsync(commentToCreate);
    
        return new Response<int>(createdCommentId, Success);
    }

    public async Task<Response<bool>> UpdateCommentAsync(CommandCommentDto request, int id)
    {
        Comment? commentToUpdate = await commentRepository.GetByIdAsync(id);
    
        if (commentToUpdate == null)
        {
            return new Response<bool>(false, NotFound);
        }
    
        CommentValidator validationRules = new();
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);
    
        if (!validationResult.IsValid)
        {
            return new Response<bool>(false, InvalidInput, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
    
        mapper.Map(request, commentToUpdate);
    
        await commentRepository.UpdateAsync(commentToUpdate);
    
        return new Response<bool>(true, Success);
    }
    
    public async Task<Response<bool>> DeleteAsync(int id)
    {
        Comment? comment = await commentRepository.GetByIdAsync(id);
    
        if (comment == null)
        {
            return new Response<bool>(false, NotFound);
        }
    
        await commentRepository.DeleteAsync(comment);
    
        return new Response<bool>(true, Success);
    }
}