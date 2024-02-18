using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

//! o handler usa o record (handle qualquer request do tipo [GetLeaveTypesQuery] e retorna tipo [LeaveTypeDto])
public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        //? query para o database
        var leaveTypes = await _leaveTypeRepository.GetAsync();

        //? converte para dto
        List<LeaveTypeDto> data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        return data;
    }
}
