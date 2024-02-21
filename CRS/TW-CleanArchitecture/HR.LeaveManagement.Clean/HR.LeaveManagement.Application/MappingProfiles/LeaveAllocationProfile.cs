using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Query.GetLeaveAllocations;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationsDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocationsDto, LeaveAllocation>();
        //CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        //CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
    }
}
