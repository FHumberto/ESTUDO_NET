namespace ScalarApiLabs.Exceptions;

public sealed class BusinessException(string message) : Exception(message);