using AutoMapper;
using FluentValidation.Results;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Validators;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class TagService(ITagRepository tagRepository, IMapper mapper)
{
    public async Task<Response<IReadOnlyList<QueryTagDto>>> GetAllAsync()
    {
        IReadOnlyList<Tag> query = await tagRepository.GetAllAsync();
        IReadOnlyList<QueryTagDto>? response = mapper.Map<IReadOnlyList<QueryTagDto>>(query);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryTagDto>>(result: new List<QueryTagDto>(), type: NotFound)
            : new Response<IReadOnlyList<QueryTagDto>>(result: response, type: Success);
    }

    public async Task<Response<QueryTagDto?>> GetByIdAsync(int id)
    {
        Tag? tag = await tagRepository.GetByIdAsync(id);
        QueryTagDto? response = mapper.Map<QueryTagDto>(tag);

        return response == null
            ? new Response<QueryTagDto?>(result: null, type: NotFound)
            : new Response<QueryTagDto?>(result: response, type: Success);
    }
    
    public async Task<Response<int>> CreateAsync(CommandTagDto request)
    {
        TagValidator validationRules = new(tagRepository);
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new Response<int>(result: 0, type: ResponseTypeEnum.InvalidInput, errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        Tag tagToCreate = mapper.Map<Tag>(request);
        int createdTagId = await tagRepository.CreateAsync(tagToCreate);

        return new Response<int>(result: createdTagId, type: ResponseTypeEnum.Success);
    }
    
    public async Task<Response<bool>> UpdateAsync(CommandTagDto request, int id)
    {
        Tag? tagToUpdate = await tagRepository.GetByIdAsync(id);
        
        if (tagToUpdate == null)
        {
            return new Response<bool>(result: false, type: NotFound);
        }
        
        TagValidator validationRules = new(tagRepository);
        ValidationResult? validationResult = await validationRules.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return new Response<bool>(result: false, type: ResponseTypeEnum.InvalidInput, errors: validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        mapper.Map(request, tagToUpdate);
        
        await tagRepository.UpdateAsync(tagToUpdate);
        
        return new Response<bool>(result: true, type: Success);
    }

    
    public async Task<Response<bool>> DeleteAsync(int id)
    {
        Tag? tag = await tagRepository.GetByIdAsync(id);

        if (tag == null)
        {
            return new Response<bool>(result: false, type: NotFound);
        }

        await tagRepository.DeleteAsync(tag);

        return new Response<bool>(result: true, type: Success);
    }
}
