﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Query.GetLeaveAllocations;

public record GetLeaveAllocationListQuery : IRequest<List<LeaveAllocationsDto>>;
