using MediatR;
using System;
using System.Linq;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

//! define os parametros da query
public record GetLeaveTypesDetailsQuery(int Id) : IRequest<LeaveTypeDetailsDto>;
