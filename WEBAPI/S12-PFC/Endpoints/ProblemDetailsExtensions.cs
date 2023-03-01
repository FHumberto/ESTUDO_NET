using Flunt.Notifications;

using Microsoft.AspNetCore.Identity;

namespace S12_PFC.Endpoints;

public static class ProblemDetailsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        // RECEBE O ERRO E OS AGRUPA EM UM DICIONARIO, POR ERRO
        return notifications
                .GroupBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
    }

    // PADRONIZANDO OS ERROS DE EMPLOYEE
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> error)
    {

        var dictionary = new Dictionary<string, string[]>();

        // AGRUPA TODOS OS ERROS NA KEY ERROR
        dictionary.Add("Error", error.Select(e => e.Description).ToArray());

        return dictionary;
        // AGRUPA MENSAGEM POR ERRO CODE
        //return error
        //        .GroupBy(g => g.Code)
        //        .ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
    }
}
