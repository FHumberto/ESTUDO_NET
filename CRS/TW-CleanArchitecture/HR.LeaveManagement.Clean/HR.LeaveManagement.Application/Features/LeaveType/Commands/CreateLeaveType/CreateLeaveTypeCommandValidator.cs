﻿using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator : AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        // Name
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PorpertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PorpertyName} must be fewer than 70 characters");

        // DefaultDays
        RuleFor(p => p.DefaultDays)
        .LessThan(100).WithMessage("{PropertyName} cannot exceed 100")
        .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("LeaveType alredy exists");

        // Validação no Banco
        this._leaveTypeRepository = leaveTypeRepository;
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}
