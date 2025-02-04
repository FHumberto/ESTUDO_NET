using FluentValidation;

namespace T_Tier.BLL.Validators;

public class PostValidator : AbstractValidator<DAL.Entities.Post>
{
    public PostValidator()
    {
        RuleFor(p => p.Tittle)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .MaximumLength(50).WithMessage("O campo de {propertyname} deve ter no máximo 50 caracteres");

        RuleFor(p => p.Body)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .MaximumLength(255).WithMessage("O campo de {propertyname} deve ter no máximo 255 caracteres");
    }
}
