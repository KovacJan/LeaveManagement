using System.Collections.Generic;

namespace leave_management.Models
{
    public class ViewAllocationsVM
    {
        public EmployeeVM Employee { get; set; }

        public string EmployeeVM { get; set; }

        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}