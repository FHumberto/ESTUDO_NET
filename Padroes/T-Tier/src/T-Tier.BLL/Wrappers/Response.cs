namespace T_Tier.BLL.Wrappers;

public class Response<T>(T result, ResponseTypeEnum type = ResponseTypeEnum.Operation, List<string> errors = null!)
{
    public T? Result { get; set; } = result;
    public ResponseTypeEnum Type { get; set; } = type;
    public List<string> Errors { get; set; } = errors;
}