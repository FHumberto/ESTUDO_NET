using FluentValidation;
using T_Tier.BLL.DTOs.Users;

namespace T_Tier.BLL.Validators.User;

public class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequestDto>
{
    public UpdateUserRoleRequestValidator()
    {
        RuleFor(u => u.RoleName)
            .NotNull()
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
    }
}