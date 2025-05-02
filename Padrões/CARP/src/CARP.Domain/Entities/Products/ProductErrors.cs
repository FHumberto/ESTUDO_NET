using CARP.Domain.Abstractions;

namespace CARP.Domain.Entities.Products;

public static class ProductErrors
{
    public static readonly Error ProductNotFound =
        new("Error.ProductNotFound", "Produto não encontrado.");

    public static readonly Error ProductAlreadyExists =
        new("Error.ProductAlreadyExists", "O produto já existe.");

    public static readonly Error ProductNameIsNullOrEmpty =
        new("Error.ProductNameIsNullOrEmpty", "O nome do produto está nulo ou vazio.");

    public static readonly Error ProductNameTooLong =
        new("Error.ProductNameTooLong", "O nome do produto é muito longo.");

    public static readonly Error ProductNameTooShort =
        new("Error.ProductNameTooShort", "O nome do produto é muito curto, mínimo de 5 caracteres.");

    public static readonly Error ProductDescriptionIsNullOrEmpty =
        new("Error.ProductDescriptionIsNullOrEmpty", "A descrição do produto está nula ou vazia.");

    public static readonly Error ProductDescriptionIsTooShort =
        new("Error.ProductDescriptionIsTooShort", "A descrição é inválida, muito curta, mínimo de 5 caracteres.");

    public static readonly Error ProductPriceIsInvalid =
        new("Error.ProductPriceIsInvalid", "O preço do produto é inválido.");

    public static readonly Error ProductStockIsInvalid =
        new("Error.ProductStockIsInvalid", "A quantidade em estoque do produto é inválida.");

    public static readonly Error ProductCategoryNotFound =
        new("Error.ProductCategoryNotFound", "A categoria do produto não foi encontrada.");

    public static readonly Error ProductCategoryIsNull =
        new("Error.ProductCategoryIsNull", "A categoria do produto está nula.");

    public static readonly Error ProductImageNameTooLong =
        new("Error.ProductImageNameTooLong", "O nome da imagem é inválido, muito longo, máximo de 250 caracteres.");
}