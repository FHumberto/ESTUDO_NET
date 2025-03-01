using System.ComponentModel.DataAnnotations;

namespace ScalarApiLabs.Models.Dto;

public class ProductCommandRequestDto
{
    [Required(ErrorMessage = "O campo de nome é obrigatório.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public required decimal Price { get; set; }
}

