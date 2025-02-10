using FluentValidation;
using T_Tier.BLL.DTOs.Posts;

namespace T_Tier.BLL.Validators;

public class PostValidator : AbstractValidator<CommandPostDto>
{
    public PostValidator()
    {
        RuleFor(p => p.Title)
            .NotNull().WithMessage("O campo de {PropertyName} é obrigatório")
            .NotEmpty().WithMessage("O campo de {PropertyName} precisa ser fornecido")
            .MaximumLength(50).WithMessage("O campo de {PropertyName} deve ter no máximo 50 caracteres");

        RuleFor(p => p.Body)
            .NotNull().WithMessage("O campo de {PropertyName} é obrigatório")
            .NotEmpty().WithMessage("O campo de {PropertyName} precisa ser fornecido")
            .MaximumLength(255).WithMessage("O campo de {PropertyName} deve ter no máximo 255 caracteres");
    }
}