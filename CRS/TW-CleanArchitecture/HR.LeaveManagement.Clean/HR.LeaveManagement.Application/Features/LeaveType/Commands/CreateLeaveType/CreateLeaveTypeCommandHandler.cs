using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //? validação


        //? converter para tipo entidade do domínio
        Domain.LeaveType leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        //? adicionar ao database
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        //? retorna o record id
        return leaveTypeToCreate.Id;
    }
}
