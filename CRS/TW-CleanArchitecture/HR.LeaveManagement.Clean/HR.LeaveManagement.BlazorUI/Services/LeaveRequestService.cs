using AutoMapper;
using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services;

public class LeaveRequestService : BaseHttpService, ILeaveRequestService
{
    private readonly IMapper _mapper;

    public LeaveRequestService(IClient client, IMapper mapper, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task ApproveLeaveRequest(int id, bool approved)
    {
        try
        {
            ChangeLeaveRequestApprovalCommand request = new() { Approved = approved, Id = id };
            await _client.UpdateApprovalAsync(request);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Task<Response<Guid>> CancelLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest)
    {
        try
        {
            Response<Guid> response = new();
            CreateLeaveRequestCommand createLeaveRequest = _mapper.Map<CreateLeaveRequestCommand>(leaveRequest);

            await _client.LeaveRequestsPOSTAsync(createLeaveRequest);
            return response;
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public Task DeleteLeaveRequest(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList()
    {
        ICollection<LeaveRequestListDto> leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: false);

        AdminLeaveRequestViewVM model = new()
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
            PendingRequests = leaveRequests.Count(q => q.Approved == null),
            RejectedRequests = leaveRequests.Count(q => q.Approved == false),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    public async Task<LeaveRequestVM> GetLeaveRequest(int id)
    {
        await AddBearerToken();
        LeaveRequestDetailsDto leaveRequest = await _client.LeaveRequestsGETAsync(id);
        return _mapper.Map<LeaveRequestVM>(leaveRequest);
    }

    public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests()
    {
        await AddBearerToken();
        ICollection<LeaveRequestListDto> leaveRequests = await _client.LeaveRequestsAllAsync(isLoggedInUser: true);
        ICollection<LeaveAllocationsDto> allocations = await _client.LeaveAllocationsAllAsync(isLoggedInUser: true);
        EmployeeLeaveRequestViewVM model = new()
        {
            LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
            LeaveRequests = _mapper.Map<List<LeaveRequestVM>>(leaveRequests)
        };

        return model;
    }

    Task<Response<Guid>> ILeaveRequestService.ApproveLeaveRequest(int id, bool approved)
    {
        throw new NotImplementedException();
    }
}
