using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.Where(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Period == period).Any();
        }

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            return await _db.LeaveAllocations.Include(_ => _.LeaveType).Include(_ => _.Employee).ToListAsync();
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            return await _db.LeaveAllocations.Include(_ => _.LeaveType).Include(_ => _.Employee).FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeId)
        {
            var period = DateTime.Now.Year;
            return await _db.LeaveAllocations.Include(_ => _.LeaveType).Include(_ => _.Employee).Where(c => c.EmployeeId == employeeId && c.Period == period).ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByEmployeeAndType(string employeeId, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            return await _db.LeaveAllocations.Include(_ => _.LeaveType).Include(_ => _.Employee).FirstOrDefaultAsync(c => c.EmployeeId == employeeId && c.Period == period && c.LeaveTypeId == leaveTypeId);
        }

        public async Task<bool> IsExists(int id)
        {
            return await _db.LeaveAllocations.AnyAsync(_ => _.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}