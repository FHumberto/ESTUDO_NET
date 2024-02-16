using MediatR;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

//! O IRequest representa o que vai retornar dessa query
public record GetLeaveTypesQuery : IRequest<List<LeaveTypeDto>>;
