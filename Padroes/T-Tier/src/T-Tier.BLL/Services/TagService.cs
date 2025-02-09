using AutoMapper;
using T_Tier.API.Wrappers;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;

namespace T_Tier.BLL.Services;

public class TagService(ITagRepository tagRepository, IMapper mapper)
{
    public async Task<Response<IReadOnlyList<QueryTagDto>>> GetAllAsync()
    {
        IReadOnlyList<Tag> query = await tagRepository.GetAllAsync();
        IReadOnlyList<QueryTagDto>? response = mapper.Map<IReadOnlyList<QueryTagDto>>(query);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryTagDto>>(new List<QueryTagDto>(), ResponseTypeEnum.NotFound)
            : new Response<IReadOnlyList<QueryTagDto>>(response, ResponseTypeEnum.Success);
    }
    
    public async Task<Response<QueryTagDto?>> GetByIdAsync(int id)
    {
        Tag? tag = await tagRepository.GetByIdAsync(id);
        QueryTagDto? response = mapper.Map<QueryTagDto>(tag);

        return response == null
            ? new Response<QueryTagDto?>(null, ResponseTypeEnum.NotFound)
            : new Response<QueryTagDto?>(response, ResponseTypeEnum.Success);
    }
}
