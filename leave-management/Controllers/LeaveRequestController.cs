using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<Employee> userManager
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequestController
        public async Task<ActionResult> Index()
        {
            var leaveRequests = await _unitOfWork.LeaveRequests.FindAll(includes: new List<string>() { "RequestingEmployee", "LeaveType" });
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);
            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(_ => _.Approved == true),
                PendingRequests = leaveRequestsModel.Count(_ => _.Approved == null),
                RejectedRequests = leaveRequestsModel.Count(_ => _.Approved == false),
                LeaveRequests = leaveRequestsModel
            };

            return View(model);
        }

        // GET: LeaveRequestController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveRequests.Find(_ => _.Id == id, includes: new List<string>() { "ApprovedType", "RequestingEmployee", "LeaveType" });
            var model = _mapper.Map<LeaveRequestVM>(leaveRequest);
            return View(model);
        }

        public async Task<ActionResult> ApproveRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = await _unitOfWork.LeaveRequests.Find(_ => _.Id == id);
                var employeeId = leaveRequest.RequestingEmloyeeId;
                var leaveTypeId = leaveRequest.LeaveTypeId;
                var allocation = await _unitOfWork.LeaveAllocations.Find(_ => _.EmployeeId == employeeId && _.LeaveTypeId == leaveTypeId && _.Period == DateTime.Now.Year);
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequested;

                leaveRequest.Approved = true;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _unitOfWork.LeaveRequests.Update(leaveRequest);
                _unitOfWork.LeaveAllocations.Update(allocation);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<ActionResult> RejectRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _unitOfWork.LeaveRequests.Find(_ => _.Id == id);

                leaveRequest.Approved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateActioned = DateTime.Now;

                _unitOfWork.LeaveRequests.Update(leaveRequest);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LeaveRequestController/Create
        public async Task<ActionResult> Create()
        {
            var leaveTypes = await _unitOfWork.LeaveTypes.FindAll();
            var leaveTypeItems = leaveTypes.Select(_ => new SelectListItem {
                Text = _.Name,
                Value = _.Id.ToString()
            });

            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };

            return View(model);
        }

        // GET: LeaveRequestController/MyLeave
        public async Task<ActionResult> MyLeave()
        {
            var employee = await _userManager.GetUserAsync(User);
            var employeeId = employee.Id;
            var employeeAllocations = await _unitOfWork.LeaveAllocations.FindAll(_ => _.EmployeeId == employeeId, includes: new List<string>() { "LeaveType" });
            var employeeRequests = await _unitOfWork.LeaveRequests.FindAll(_ => _.RequestingEmloyeeId == employeeId);

            var employeeAllocationsModel = _mapper.Map<List<LeaveAllocationVM>>(employeeAllocations);
            var employeeRequestsModel = _mapper.Map<List<LeaveRequestVM>>(employeeRequests);

            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = employeeAllocationsModel,
                LeaveRequests = employeeRequestsModel
            };

            return View(model);
        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM model)
        {
            try
            {
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var leaveTypes = await _unitOfWork.LeaveTypes.FindAll();
                var leaveTypeItems = leaveTypes.Select(_ => new SelectListItem
                {
                    Text = _.Name,
                    Value = _.Id.ToString()
                });

                model.LeaveTypes = leaveTypeItems;

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be further in the future than the End Date.");
                    return View(model);
                }

                var employee = await _userManager.GetUserAsync(User);
                var allocation = await _unitOfWork.LeaveAllocations.Find(_ => _.EmployeeId == employee.Id && _.LeaveTypeId == model.LeaveTypeId && _.Period == DateTime.Now.Year);

                int daysRequested = (int)(endDate.Date - startDate.Date).TotalDays;

                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You do not have sufficient days for this request.");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestVM
                {
                    RequestingEmloyeeId = employee.Id,
                    LeaveTypeId = model.LeaveTypeId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    RequestComments = model.RequestComments
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                await _unitOfWork.LeaveRequests.Create(leaveRequest);
                await _unitOfWork.Save();
               

                return RedirectToAction(nameof(MyLeave));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong.");
                return View(model);
            }
        }

        public async Task<ActionResult> CancelRequest(int id)
        {
            var leaveRequest = await _unitOfWork.LeaveRequests.Find(_ => _.Id == id);
            leaveRequest.Cancelled = true;
            _unitOfWork.LeaveRequests.Update(leaveRequest);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(MyLeave));
        }

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
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