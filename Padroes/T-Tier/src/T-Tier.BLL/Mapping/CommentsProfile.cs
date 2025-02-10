﻿using AutoMapper;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.DAL.Entities;

namespace T_Tier.BLL.Mapping;

public class CommentsProfile : Profile
{
    public CommentsProfile()
    {
        //? converte de [A ==> B]
        CreateMap<Comment, QueryCommentDto>();
        CreateMap<Comment, QueryCommentPostDto>();
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();
    }
}