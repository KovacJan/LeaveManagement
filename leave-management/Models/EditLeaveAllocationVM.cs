using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{
    public class EditLeaveAllocationVM
    {
        public int Id { get; set; }

        public EmployeeVM Employee { get; set; }

        public string EmployeeId { get; set; }

        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; }

        public LeaveTypeVM LeaveType { get; set; }
    }
}