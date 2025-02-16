using FluentValidation;
using T_Tier.BLL.DTOs.Comments;

namespace T_Tier.BLL.Validators.Comment;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
{
    public UpdateCommentValidator()
        => RuleFor(c => c.Body)
            .NotNull().WithMessage("O campo de {PropertyName} é obrigatório")
            .NotEmpty().WithMessage("O campo de {PropertyName} precisa ser fornecido")
            .MaximumLength(255).WithMessage("O campo de {PropertyName} deve ter no máximo 255 caracteres");
}
