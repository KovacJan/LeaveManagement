using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(LeaveRequest entity)
        {
            await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveRequest>> FindAll()
        {
            return await _db.LeaveRequests
                .Include(_ => _.RequestingEmployee)
                .Include(_ => _.ApprovedBy)
                .Include(_ => _.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveRequest> FindById(int id)
        {
            return await _db.LeaveRequests
                .Include(_ => _.RequestingEmployee)
                .Include(_ => _.ApprovedBy)
                .Include(_ => _.LeaveType)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeId)
        {
            return await _db.LeaveRequests
                .Include(_ => _.RequestingEmployee)
                .Include(_ => _.ApprovedBy)
                .Include(_ => _.LeaveType)
                .Where(_ => _.RequestingEmloyeeId == employeeId)
                .ToListAsync();
        }

        public async Task<bool> IsExists(int id)
        {
            return await _db.LeaveRequests.AnyAsync(_ => _.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }
    }
}