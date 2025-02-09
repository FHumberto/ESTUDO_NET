namespace T_Tier.API.Wrappers;

public class Response<T>
{
    public Response() { }

    public Response(T result, ResponseTypeEnum type)
    {
        Result = result;
        Type = type;
    }

    public T Result { get; set; }

    public ResponseTypeEnum Type { get; set; }
}

