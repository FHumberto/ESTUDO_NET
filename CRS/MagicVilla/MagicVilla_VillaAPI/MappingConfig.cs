﻿using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDto>();
        CreateMap<VillaDto, Villa>();

        CreateMap<Villa, VillaCreateDto>().ReverseMap();
        CreateMap<Villa, VillaUpdateDto>().ReverseMap();

        // VillaNumber
        CreateMap<VillaNumber, VillaNumberDto>();

        CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();

        //identity map
        CreateMap<ApplicationUser, UserDto>().ReverseMap();
    }
}
