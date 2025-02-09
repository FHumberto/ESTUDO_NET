namespace T_Tier.API.Wrappers
{
    public class Response<T>(T result, ResponseTypeEnum type = ResponseTypeEnum.Success, List<string> errors = null)
    {
        public T? Result { get; set; } = result;
        public ResponseTypeEnum Type { get; set; } = type;
        public List<string> Errors { get; set; } = errors ?? new List<string>();
    }
}