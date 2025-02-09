namespace T_Tier.API.Wrappers;

public class Response<T>(T result, ResponseTypeEnum type = ResponseTypeEnum.Success)
{
    public T? Result { get; set; } = result;

    public ResponseTypeEnum Type { get; set; } = type;
}

