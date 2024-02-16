using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypesDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypesDetailsQuery request, CancellationToken cancellationToken)
    {
        // query
        Domain.LeaveType leaveTypesDetail = await _leaveTypeRepository.GetByIdAsync(request.Id);

        //? validação
        if (leaveTypesDetail is null)
        {
            throw new NotFoundException(nameof(LeaveType), request.Id);
        }

        // conversão
        LeaveTypeDetailsDto data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypesDetail);

        return data;
    }
}
