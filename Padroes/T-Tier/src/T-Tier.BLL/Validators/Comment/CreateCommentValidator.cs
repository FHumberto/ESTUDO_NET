using FluentValidation;
using T_Tier.BLL.DTOs.Comments;
using T_Tier.DAL.Contracts;

namespace T_Tier.BLL.Validators.Comment;

public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentValidator(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;

        RuleFor(c => c.PostId)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido.")
            .NotNull().WithMessage("O campo {PropertyName} é obrigatório.")
            .Must(DoesPostExist).WithMessage("Um comentário só pode ser cadastrado se o post existir.");

        RuleFor(c => c.Body)
            .NotNull().WithMessage("O campo de {PropertyName} é obrigatório")
            .NotEmpty().WithMessage("O campo de {PropertyName} precisa ser fornecido")
            .MaximumLength(255).WithMessage("O campo de {PropertyName} deve ter no máximo 255 caracteres");
    }

    private bool DoesPostExist(int postId)
    {
        return _commentRepository.GetByIdAsync(postId).Result != null;
    }
}
