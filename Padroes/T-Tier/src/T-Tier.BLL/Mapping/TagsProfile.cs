using AutoMapper;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.DAL.Entities;

namespace T_Tier.BLL.Mapping;

public class TagsProfile : Profile
{
    public TagsProfile()
    {
        //? converte de [A ==> B]
        CreateMap<Tag, QueryTagResponseDto>();
        CreateMap<CommandTagDto, Tag>();
    }
}
