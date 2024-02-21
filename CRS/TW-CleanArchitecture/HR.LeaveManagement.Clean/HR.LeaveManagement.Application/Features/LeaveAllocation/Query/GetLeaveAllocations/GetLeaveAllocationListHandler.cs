using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Query.GetLeaveAllocations;

public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationsDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocation;

    public GetLeaveAllocationListHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocation)
    {
        _mapper = mapper;
        _leaveAllocation = leaveAllocation;
    }

    public async Task<List<LeaveAllocationsDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        //! TODO
        //- records de um usuário específico
        //- get allocations por employee
        IReadOnlyList<Domain.LeaveAllocation> leaveAllocations = await _leaveAllocation.GetAsync();

        List<LeaveAllocationsDto> data = _mapper.Map<List<LeaveAllocationsDto>>(leaveAllocations);

        return data;
    }
}
