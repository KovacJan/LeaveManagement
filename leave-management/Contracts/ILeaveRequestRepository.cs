using leave_management.Data;
using System.Collections.Generic;

namespace leave_management.Contracts
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        List<LeaveRequest> GetLeaveRequestsByEmployee(string employeeId);
    }
}