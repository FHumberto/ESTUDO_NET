using CARP.Domain.Abstractions;

namespace CARP.Domain.Categories;

public static class CategoryErrors
{
    public static readonly Error CategoryNotFound =
        new("Error.CategoryNotFound", "Categoria não encontrada.");

    public static readonly Error CategoryAlreadyExists =
        new("Error.CategoryAlreadyExists", "Categoria já existe.");

    public static readonly Error CategoryNameIsNullOrEmpty =
        new("Error.CategoryNameIsNullOrEmpty", "O nome da categoria está nulo ou vazio.");

    public static readonly Error CategoryNameTooShort =
        new("Error.CategoryNameTooShort", "O nome da categoria é menor que 3 caracteres.");

    public static readonly Error CategoryNameTooLong =
        new("Error.CategoryNameTooLong", "O nome da categoria é muito longo.");
}