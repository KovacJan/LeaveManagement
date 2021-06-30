using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();
        }

        public ICollection<LeaveRequest> FindAll()
        {
            return _db.LeaveRequests
                .Include(_ => _.RequestingEmployee)
                .Include(_ => _.ApprovedBy)
                .Include(_ => _.LeaveType)
                .ToList();
        }

        public LeaveRequest FindById(int id)
        {
            return _db.LeaveRequests
                .Include(_ => _.RequestingEmployee)
                .Include(_ => _.ApprovedBy)
                .Include(_ => _.LeaveType)
                .FirstOrDefault(_ => _.Id == id);
        }

        public List<LeaveRequest> GetLeaveRequestsByEmployee(string employeeId)
        {
            return _db.LeaveRequests
                .Include(_ => _.RequestingEmployee)
                .Include(_ => _.ApprovedBy)
                .Include(_ => _.LeaveType)
                .Where(_ => _.RequestingEmloyeeId == employeeId)
                .ToList();
        }

        public bool IsExists(int id)
        {
            return _db.LeaveRequests.Any(_ => _.Id == id);
        }

        public bool Save()
        {
            var changes = _db.SaveChanges();
            return changes > 0;
        }

        public bool Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
        }
    }
}