using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationController(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<Employee> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocationController
        public async Task<ActionResult> Index()
        {
            //var leaveTypes = await _leaveRepo.FindAll();
            var leaveTypes = await _unitOfWork.LeaveTypes.FindAll();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes.ToList());
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };

            return View(model);
        }

        public async Task<ActionResult> SetLeave(int id)
        {
            //var leaveType = await _leaveRepo.FindById(id);
            var leaveType = await _unitOfWork.LeaveTypes.Find(_ => _.Id == id);
            var employees = await _userManager.GetUsersInRoleAsync("Employee");

            foreach (var employee in employees)
            {
                //if (await _leaveAllocationRepo.CheckAllocation(id, employee.Id))
                if (await _unitOfWork.LeaveAllocations.IsExists(_ => _.EmployeeId == employee.Id && _.LeaveTypeId == id && _.Period == DateTime.Now.Year))
                {
                    continue;
                }

                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year
                };

                var leaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                //await _leaveAllocationRepo.Create(leaveAllocation);
                await _unitOfWork.LeaveAllocations.Create(leaveAllocation);
                await _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> ListEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeVM>>(employees);

            return View(model);
        }

        // GET: LeaveAllocationController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var employee = _mapper.Map<EmployeeVM>(await _userManager.FindByIdAsync(id));
            //var allocations = _mapper.Map<List<LeaveAllocationVM>>(await _leaveAllocationRepo.GetLeaveAllocationsByEmployee(id));
            var records = _unitOfWork.LeaveAllocations.FindAll(_ => _.EmployeeId == id && _.Period == DateTime.Now.Year, includes: new List<string>() { "LeaveType" });
            var allocations = _mapper.Map<List<LeaveAllocationVM>>(records);

            var model = new ViewAllocationsVM
            {
                Employee = employee,
                LeaveAllocations =  allocations
            };

            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveAllocation = _mapper.Map<EditLeaveAllocationVM>(await _unitOfWork.LeaveAllocations.Find(_ => _.Id == id));
            return View(leaveAllocation);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //var record = await _leaveAllocationRepo.FindById(model.Id);
                var record = await _unitOfWork.LeaveAllocations.Find(_ => _.Id == model.Id);
                record.NumberOfDays = model.NumberOfDays;
                //var isSuccess = await _leaveAllocationRepo.Update(record);
                _unitOfWork.LeaveAllocations.Update(record);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View(model);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}