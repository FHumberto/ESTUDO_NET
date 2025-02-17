#region

using AutoMapper;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

#endregion

namespace T_Tier.BLL.Services;

public class PostService(IPostRepository postRepository, IUserService userService, IServiceProvider serviceProvider, IMapper mapper) : IPostService
{
    public async Task<Response<IReadOnlyList<QueryPostResponseDto>>> GetAllPost()
    {
        IReadOnlyList<Post> posts = await postRepository.GetAllAsync();
        IReadOnlyList<QueryPostResponseDto> response = mapper.Map<IReadOnlyList<QueryPostResponseDto>>(posts);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryPostResponseDto>>([], NotFound)
            : new Response<IReadOnlyList<QueryPostResponseDto>>(response, Success);
    }

    public async Task<Response<QueryPostResponseDto?>> GetPostById(int id)
    {
        Post? post = await postRepository.GetByIdAsync(id);
        QueryPostResponseDto? response = mapper.Map<QueryPostResponseDto>(post);

        return response == null
            ? new Response<QueryPostResponseDto?>(null, NotFound, "Postagem não localizada.")
            : new Response<QueryPostResponseDto?>(response, Success);
    }

    public async Task<Response<QueryPostTagResponseDto?>> GetPostByIdWithTag(int id)
    {
        Post? post = await postRepository.GetPostByIdWithTagsAsync(id);
        QueryPostTagResponseDto? response = mapper.Map<QueryPostTagResponseDto>(post);

        return response == null
            ? new Response<QueryPostTagResponseDto?>(null, NotFound, "Postagem não localizada.")
            : new Response<QueryPostTagResponseDto?>(response, Success);
    }

    public async Task<Response<QueryPostCommentsResponseDto?>> GetPostByIdWithComments(int id)
    {
        Post? post = await postRepository.GetPostByidWithCommentsAsync(id);
        QueryPostCommentsResponseDto? response = mapper.Map<QueryPostCommentsResponseDto>(post);

        return response == null
            ? new Response<QueryPostCommentsResponseDto?>(null, NotFound, "Postagem não localizada.")
            : new Response<QueryPostCommentsResponseDto?>(response, Success);
    }

    public async Task<Response<int>> CreatePost(CommandPostRequestDto request)
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
        {
            throw new UnauthorizedAccessException();
        }

        #endregion

        #region ====[3.MAPEAMENTO]======================================================================================

        Post? postToCreate = mapper.Map<Post>(request);

        postToCreate.UserId = userId;
        postToCreate.CreatedBy = userId;

        #endregion

        #region ====[4.ACÃO]============================================================================================

        var createdPostId = await postRepository.CreateAsync(postToCreate);

        return new Response<int>(createdPostId, Success);

        #endregion
    }

    public async Task<Response<bool>> UpdatePost(CommandPostRequestDto request, int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var postToUpdate = await postRepository.GetByIdAsync(id);

        if (postToUpdate is null)
        {
            return new Response<bool>(false, NotFound, "Postagem não encontrada.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        if (userService.UserId != postToUpdate.UserId)
        {
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3.MAPEAMENTO]======================================================================================

        mapper.Map(request, postToUpdate);

        postToUpdate.UpdatedBy = userService.UserId;

        #endregion

        #region ====[4.ACÃO]============================================================================================

        await postRepository.UpdateAsync(postToUpdate);

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> DeletePostById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var post = await postRepository.GetByIdAsync(id);

        if (post is null)
        {
            return new Response<bool>(false, NotFound, "Postagem não encontrada.");
        }

        #endregion

        #region ====[2.ACÃO]============================================================================================

        await postRepository.DeleteAsync(post);

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> SoftDeletePostById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var postToDelete = await postRepository.GetByIdAsync(id);

        if (postToDelete is null)
        {
            return new Response<bool>(false, NotFound, "Comentário não encontrado.");
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        if (postToDelete.UserId != userService.UserId)
        {
            throw new InvalidOperationException("Operação Inválida.");
        }

        #endregion

        #region ====[3.ACÃO]============================================================================================

        var operation = postRepository.SoftDeleteAsync(postToDelete);

        return operation.Result
            ? new Response<bool>(true, Success)
            : new Response<bool>(true, Error, "Erro durante a operação de deletar a postagem.");

        #endregion
    }
}