using System.ComponentModel.DataAnnotations;

namespace ScalarApiLabs.Exceptions;

public class CustomValidationException : ValidationException
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException(IDictionary<string, string[]> errors)
        : base("Erros de validação encontrados.")
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
    }
}
