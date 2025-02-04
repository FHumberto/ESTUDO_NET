using FluentValidation;

namespace T_Tier.BLL.Validators;

public class TagValidator : AbstractValidator<DAL.Entities.Tag>
{
    public TagValidator()
    {
        RuleFor(t => t.Name)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .Length(1, 50).WithMessage("O campo de {propertyname} deve ter entre 1 e 50 caracteres");

        // TODO: adicionar busca pra garantir que é úncio
    }
}
