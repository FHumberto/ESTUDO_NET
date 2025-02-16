using FluentValidation;
using T_Tier.BLL.DTOs.Users;

namespace T_Tier.BLL.Validators.User;

public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(7).WithMessage("O campo {PropertyName} deve ter pelo menos 7 caracteres.")
            .EmailAddress().WithMessage("O campo {PropertyName} deve conter um endereço de e-mail válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(4).WithMessage("O campo {PropertyName} deve ter pelo menos 4 caracteres.");
    }
}