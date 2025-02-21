using FluentValidation;
using T_Tier.DAL.Contracts;

public class TagPostValidator : AbstractValidator<CommandTagPostRequest>
{
    private readonly ITagRepository _tagRepository;

    public TagPostValidator(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;

        RuleFor(x => x.TagIds)
            .NotEmpty().WithMessage("A lista de tags não pode estar vazia.")
            .MustAsync(AllTagsExist).WithMessage("Uma ou mais tags não existem no banco.");
    }

    private async Task<bool> AllTagsExist(List<int> tagIds, CancellationToken cancellationToken)
    {
        if (tagIds == null || tagIds.Count == 0)
        {
            return false;
        }

        var existingTags = await _tagRepository.GetByIdsAsync(tagIds);
        return existingTags.Count == tagIds.Count;
    }
}
