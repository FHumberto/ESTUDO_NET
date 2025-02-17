using AutoMapper;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.BLL.Interfaces;
using T_Tier.BLL.Utils;
using T_Tier.BLL.Wrappers;
using T_Tier.DAL.Contracts;
using T_Tier.DAL.Entities;
using static T_Tier.BLL.Wrappers.ResponseTypeEnum;

namespace T_Tier.BLL.Services;

public class TagService(ITagRepository tagRepository, IServiceProvider serviceProvider, IMapper mapper) : ITagService
{
    public async Task<Response<IReadOnlyList<QueryTagResponseDto>>> GetAllTag()
    {
        IReadOnlyList<Tag> tags = await tagRepository.GetAllAsync();
        IReadOnlyList<QueryTagResponseDto> response = mapper.Map<IReadOnlyList<QueryTagResponseDto>>(tags);

        return response == null || response.Count == 0
            ? new Response<IReadOnlyList<QueryTagResponseDto>>([], type: NotFound)
            : new Response<IReadOnlyList<QueryTagResponseDto>>(response, Success);
    }

    public async Task<Response<QueryTagResponseDto?>> GetTagById(int id)
    {
        Tag? tag = await tagRepository.GetByIdAsync(id);
        QueryTagResponseDto? response = mapper.Map<QueryTagResponseDto>(tag);

        return response == null
            ? new Response<QueryTagResponseDto?>(null, NotFound)
            : new Response<QueryTagResponseDto?>(response, Success);
    }

    public async Task<Response<int>> CreateTag(CommandTagDto request)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            return new Response<int>(0, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.MAPEAMENTO]======================================================================================

        Tag tagToCreate = mapper.Map<Tag>(request);

        #endregion

        #region ====[3.ACÃO]============================================================================================

        int createdTagId = await tagRepository.CreateAsync(tagToCreate);

        return new Response<int>(createdTagId, ResponseTypeEnum.Success);

        #endregion
    }

    public async Task<Response<bool>> UpdateTag(CommandTagDto request, int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var tagToUpdate = await tagRepository.GetByIdAsync(id);

        if (tagToUpdate is null)
        {
            return new Response<bool>(false, NotFound, "Tag não encontrada.");
        }

        var validationResult = await serviceProvider.ValidateAsync(request);

        if (validationResult.Count > 0)
        {
            return new Response<bool>(false, InvalidInput, validationResult);
        }

        #endregion

        #region ====[2.REGRA]===========================================================================================

        mapper.Map(request, tagToUpdate);

        #endregion

        #region ====[3.ACÃO]============================================================================================

        await tagRepository.UpdateAsync(tagToUpdate);

        return new Response<bool>(true, Success);

        #endregion
    }

    public async Task<Response<bool>> DeleteTagById(int id)
    {
        #region ====[1.VALIDAÇÃO]=======================================================================================

        var tagToDelete = await tagRepository.GetByIdAsync(id);

        if (tagToDelete is null)
        {
            return new Response<bool>(false, NotFound, "Tag não encontrada.");
        }

        #endregion

        #region ====[2.ACÃO]============================================================================================

        await tagRepository.DeleteAsync(tagToDelete);

        return new Response<bool>(true, Success);

        #endregion
    }
}
