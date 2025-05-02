namespace CARP.Domain.Exceptions;

public class DomainValidationException(string error) : Exception(error)
{
    public static void When(bool hasError, string error)
    {
        if (hasError)
        {
            throw new DomainValidationException(error);
        }
    }
}
