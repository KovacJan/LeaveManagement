﻿using System;

namespace leave_management.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }

        public int NumberOfDays { get; set; }

        public DateTime DateCreated { get; set; }

        public int Period { get; set; }

        public EmployeeVM Employee { get; set; }

        public string EmployeeId { get; set; }

        public LeaveTypeVM LeaveType { get; set; }

        public int LeaveTypeId { get; set; }
    }
}