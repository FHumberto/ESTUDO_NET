using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestsWithDetails(int id);
    Task<List<LeaveRequest>> GetAllLeaveRequestsWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetials(string userId);
}