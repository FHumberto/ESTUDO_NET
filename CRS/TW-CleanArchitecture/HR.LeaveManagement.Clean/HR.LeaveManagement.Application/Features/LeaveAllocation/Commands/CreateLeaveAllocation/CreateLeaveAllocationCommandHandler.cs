using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IUserService userService)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        //? validação
        CreateLeaveAllocationCommandValidator validator = new(_leaveTypeRepository);
        ValidationResult validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            //! lista de erros de validação
            throw new BadRequestException("Invalid Leave Allocation Request", validationResult);

        //? query
        Domain.LeaveType leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        List<Models.Identity.Employee> employees = await _userService.GetEmployees();

        int period = DateTime.Now.Year;

        //Assign Allocations IF an allocation doesn't already exist for period and leave type
        List<Domain.LeaveAllocation> allocations = new();

        foreach (Models.Identity.Employee emp in employees)
        {
            bool allocationExists = await _leaveAllocationRepository.AllocationExists(emp.Id, request.LeaveTypeId, period);

            if (allocationExists == false)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = emp.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period,
                });
            }
        }

        if (allocations.Any())
        {
            await _leaveAllocationRepository.AddAllocations(allocations);
        }

        return Unit.Value;
    }
}
