using AutoMapper;
using T_Tier.BLL.DTOs.Posts;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class PostService(IPostRepository postRepository, IMapper mapper)
{
    public async Task<Response<QueryPostDto?>> GetByIdAsync(int id)
    {
        Post? post = await postRepository.GetByIdAsync(id);
        QueryPostDto? response = mapper.Map<QueryPostDto>(post);

        return response == null
            ? new Response<QueryPostDto?>(result: null, type: NotFound)
            : new Response<QueryPostDto?>(result: response, type: Success);
    }
}