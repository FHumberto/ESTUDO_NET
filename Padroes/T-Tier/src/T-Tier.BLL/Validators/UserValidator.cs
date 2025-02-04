using FluentValidation;

namespace T_Tier.BLL.Validators;

public class UserValidator : AbstractValidator<DAL.Entities.User>
{
    public UserValidator()
    {
        RuleFor(u => u.FirstName)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .MaximumLength(50).WithMessage("O campo de {propertyname} deve ter no máximo 50 caracteres");

        RuleFor(u => u.LastName)
            .MaximumLength(50).WithMessage("O campo de {propertyname} deve ter no máximo 50 caracteres");

        RuleFor(u => u.Email)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido")
            .MaximumLength(100).WithMessage("O campo de {propertyname} deve ter no máximo 100 caracteres")
            .EmailAddress().WithMessage("O campo de {propertyname} deve ser um email válido");

        RuleFor(u => u.PasswordHash)
            .NotNull().WithMessage("O campo de {propertyname} é obrigatório")
            .NotEmpty().WithMessage("O campo de {propertyname} precisa ser fornecido");
    }
}
