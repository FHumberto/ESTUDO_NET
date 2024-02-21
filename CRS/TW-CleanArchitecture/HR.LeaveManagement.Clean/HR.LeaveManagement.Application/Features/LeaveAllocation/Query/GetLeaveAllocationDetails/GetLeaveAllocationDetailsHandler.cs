using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Query.GetLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Query.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationDetailsHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        //? query
        var leaveAllocationDetails = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        //? validação
        if (leaveAllocationDetails is null)
        {
            throw new NotFoundException(nameof(leaveAllocationDetails), request.Id);
        }

        //? conversão
        LeaveAllocationDetailsDto data = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocationDetails);

        return data;
    }
}
