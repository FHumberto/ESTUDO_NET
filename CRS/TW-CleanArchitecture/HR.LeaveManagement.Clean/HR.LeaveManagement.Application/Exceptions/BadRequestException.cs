﻿using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = new();

        foreach (ValidationFailure error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }

    public List<string> ValidationErrors { get; set; }
}
