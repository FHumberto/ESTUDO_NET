using Flunt.Notifications;

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
}
