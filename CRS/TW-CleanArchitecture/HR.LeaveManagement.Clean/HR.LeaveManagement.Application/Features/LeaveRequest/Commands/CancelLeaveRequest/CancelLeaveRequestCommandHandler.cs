using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        //? validação
        Domain.LeaveRequest leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.Id);

        //? query
        leaveRequest.Cancelled = true;

        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        if (leaveRequest.Approved == true)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            allocation.NumberOfDays += daysRequested;

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        //? execução
        try
        {
            EmailMessage email = new()
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully.",
                Subject = "Leave Request Cancelled"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception)
        {
            // log error
        }
        
        return Unit.Value;
    }
}