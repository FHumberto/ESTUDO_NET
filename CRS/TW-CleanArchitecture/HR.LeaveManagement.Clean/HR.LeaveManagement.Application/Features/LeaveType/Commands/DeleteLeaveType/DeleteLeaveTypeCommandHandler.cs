using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //? recuperar a entidade no banco
        Domain.LeaveRequest leaveTypeToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

        //? validação
        if (leaveTypeToDelete is null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        //? remove do banco
        await _leaveRequestRepository.DeleteAsync(leaveTypeToDelete);

        //? retorna o id
        return Unit.Value;
    }
}
