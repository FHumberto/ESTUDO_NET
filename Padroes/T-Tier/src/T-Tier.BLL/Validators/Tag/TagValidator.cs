using FluentValidation;
using T_Tier.BLL.DTOs.Tags;
using T_Tier.DAL.Contracts;

namespace T_Tier.BLL.Validators.Tag;

public class TagValidator : AbstractValidator<CommandTagDto>
{
    private readonly ITagRepository _tagRepository;

    public TagValidator(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;

        RuleFor(t => t.Name)
            .NotNull().WithMessage("O campo de {PropertyName} é obrigatório")
            .NotEmpty().WithMessage("O campo de {PropertyName} precisa ser fornecido")
            .Length(1, 50).WithMessage("O campo de {PropertyName} deve ter entre 1 e 50 caracteres")
            .Must(IsUnique).WithMessage("O campo de {PropertyName} precisa ser único");
    }

    private bool IsUnique(string name)
    {
        return _tagRepository.GetByNameAsync(name).Result == null;
    }
}
