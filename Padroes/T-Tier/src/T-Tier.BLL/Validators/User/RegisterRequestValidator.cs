using FluentValidation;
using T_Tier.BLL.DTOs.Users;

namespace T_Tier.BLL.Validators.User;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(2).WithMessage("O campo {PropertyName} deve ter pelo menos 2 caracteres.");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(2).WithMessage("O campo {PropertyName} deve ter pelo menos 2 caracteres.");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(7).WithMessage("O campo {PropertyName} deve ter pelo menos 7 caracteres.")
            .EmailAddress().WithMessage("O campo {PropertyName} deve conter um endereço de e-mail válido.");

        RuleFor(r => r.UserName)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(3).WithMessage("O campo {PropertyName} deve ter pelo menos 3 caracteres.");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
            .MinimumLength(6).WithMessage("O campo {PropertyName} deve ter pelo menos 6 caracteres.")
            .Matches(@"[A-Z]").WithMessage("O campo {PropertyName} deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("O campo {PropertyName} deve conter pelo menos uma letra minúscula.")
            .Matches(@"\d").WithMessage("O campo {PropertyName} deve conter pelo menos um número.")
            .Matches(@"[\W_]").WithMessage("O campo {PropertyName} deve conter pelo menos um caractere especial.");
    }
}