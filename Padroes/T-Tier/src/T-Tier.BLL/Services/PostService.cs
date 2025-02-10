#region

using AutoMapper;
using FluentValidation.Results;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Validators;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

#endregion

namespace T_Tier.BLL.Services;

public class PostService(IPostRepository postRepository, IMapper mapper)
{
    public async Task<Response<IReadOnlyList<QueryPostDto>>> GetAllPostAsync()
    {
        IReadOnlyList<Post> posts = await postRepository.GetAllAsync();
        IReadOnlyList<QueryPostDto> response = mapper.Map<IReadOnlyList<QueryPostDto>>(posts);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryPostDto>>(new List<QueryPostDto>(), NotFound)
            : new Response<IReadOnlyList<QueryPostDto>>(response, Success);
    }

    public async Task<Response<QueryPostDto?>> GetPostByIdAsync(int id)
    {
        Post? post = await postRepository.GetByIdAsync(id);
        QueryPostDto? response = mapper.Map<QueryPostDto>(post);

        return response == null
            ? new Response<QueryPostDto?>(null, NotFound)
            : new Response<QueryPostDto?>(response, Success);
    }

    /// <summary>
    ///     Retorna um post com suas tags
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Response<QueryPostTagDto?>> GetPostByIdWithTagAsync(int id)
    {
        Post? post = await postRepository.GetPostByIdWithTagAsync(id);
        QueryPostTagDto? response = mapper.Map<QueryPostTagDto>(post);

        return response == null
            ? new Response<QueryPostTagDto?>(null, NotFound)
            : new Response<QueryPostTagDto?>(response, Success);
    }

    public async Task<Response<int>> CreatePostAsync(CommandPostDto request)
    {
        PostValidator validationRules = new();
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new Response<int>(0, InvalidInput, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        Post postToCreate = mapper.Map<Post>(request);
        int createdPostId = await postRepository.CreateAsync(postToCreate);

        return new Response<int>(createdPostId, Success);
    }

    public async Task<Response<bool>> UpdatePostAsync(CommandPostDto request, int id)
    {
        Post? postToUpdate = await postRepository.GetByIdAsync(id);

        if (postToUpdate == null)
        {
            return new Response<bool>(false, NotFound);
        }

        PostValidator validationRules = new();
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new Response<bool>(false, InvalidInput, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        mapper.Map(request, postToUpdate);

        await postRepository.UpdateAsync(postToUpdate);

        return new Response<bool>(true, Success);
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        Post? post = await postRepository.GetByIdAsync(id);

        if (post == null)
        {
            return new Response<bool>(false, NotFound);
        }

        await postRepository.DeleteAsync(post);

        return new Response<bool>(true, Success);
    }
}