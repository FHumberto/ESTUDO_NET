using System.Text.Json.Serialization;

namespace T_Tier.BLL.Wrappers;

public class Response<T>(
    T result,
    ResponseTypeEnum type = ResponseTypeEnum.Operation,
    object? errors = null) // Pode ser List<string> ou Dictionary<string, List<string>>
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Result { get; init; } = result;

    public ResponseTypeEnum Type { get; init; } = type;

    //? Erros gerais (exemplo: falha na autenticação, erro interno, etc.)
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Errors { get; init; } =
        //? Se 'errors' for do tipo List<string> e não estiver vazia, atribui à propriedade Errors. Caso contrário, mantém como null.
        errors is List<string> list && list.Count > 0 ? list : null;

    //? Erros de validação (exemplo: campos inválidos, mensagens específicas por propriedade)
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, List<string>>? ValidationErrors { get; init; } =
        //? Se 'errors' for do tipo Dictionary<string, List<string>> e não estiver vazio, atribui à propriedade ValidationErrors. Caso contrário, mantém como null.
        errors is Dictionary<string, List<string>> dict && dict.Count > 0 ? dict : null;
}