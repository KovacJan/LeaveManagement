using System.Collections.Generic;

namespace leave_management.Models
{
    public class CreateLeaveAllocationVM
    {
        public List<LeaveTypeVM> LeaveTypes { get; set; }

        public int NumberUpdated { get; set; }
    }
}