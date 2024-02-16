
namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, object key) : base($"{message}")
    {

    }
}
