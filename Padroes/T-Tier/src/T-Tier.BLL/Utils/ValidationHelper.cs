using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace T_Tier.BLL.Utils;

public static class ValidationHelper
{
    public static async Task<Dictionary<string, List<string>>> ValidateAsync<T>(this IServiceProvider serviceProvider, T request)
    {
        var validator = serviceProvider.GetService<IValidator<T>>();
        if (validator is null) return new Dictionary<string, List<string>>();

        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.IsValid) return new Dictionary<string, List<string>>();

        return validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToList()
            );
    }
}