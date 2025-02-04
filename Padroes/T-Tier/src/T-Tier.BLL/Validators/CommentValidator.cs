using FluentValidation;

namespace T_Tier.BLL.Validators;

public class CommentValidator : AbstractValidator<DAL.Entities.Comment>
{
    public CommentValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido");

        RuleFor(c => c.PostId)
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório");

        RuleFor(c => c.Body)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .MaximumLength(255).WithMessage("O campo de {propertyname} deve ter no máximo 255 caracteres");
    }
}
