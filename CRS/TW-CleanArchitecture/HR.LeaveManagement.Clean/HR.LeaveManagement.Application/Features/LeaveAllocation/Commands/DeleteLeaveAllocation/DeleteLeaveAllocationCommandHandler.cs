using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        //? query
        var leaveAllocationDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        //? validação
        if (leaveAllocationDelete is null)
            throw new NotFoundException(nameof(leaveAllocationDelete), request.Id);

        //? execução
        await _leaveAllocationRepository.DeleteAsync(leaveAllocationDelete);

        //? retorna o id
        return Unit.Value;
    }
}
