using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LeaveTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
            //var leaveTypes = await _repo.FindAll();
            var leaveTypes = await _unitOfWork.LeaveTypes.FindAll();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leaveTypes.ToList());

            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //var isExist = await _repo.IsExists(id);
            var isExist = await _unitOfWork.LeaveTypes.IsExists(_ => _.Id == id);

            if (!isExist)
            {
                return NotFound();
            }

            //var leaveType = await _repo.FindById(id);
            var leaveType = await _unitOfWork.LeaveTypes.Find(_ => _.Id == id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);

            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);                    
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;

                //var isSuccess = await _repo.Create(leaveType);
                await _unitOfWork.LeaveTypes.Create(leaveType);
                await _unitOfWork.Save();

                //if (!isSuccess)
                //{
                //    ModelState.AddModelError("", "Something went wrong.");
                //    return View(model);
                //}

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong.");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //var isExist = await _repo.IsExists(id);
            await _unitOfWork.LeaveTypes.IsExists(_ => _.Id == id);

            //if (!isExist)
            //{
            //    return NotFound();
            //}

            //var leaveType = await _repo.FindById(id);
            var leaveType = await _unitOfWork.LeaveTypes.Find(_ => _.Id == id);
            var model = _mapper.Map<LeaveTypeVM>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveType = _mapper.Map<LeaveType>(model);
                //var isSuccess = await _repo.Update(leaveType);

                //if (!isSuccess)
                //{
                //    ModelState.AddModelError("", "Something went wrong.");
                //    return View(model);
                //}

                _unitOfWork.LeaveTypes.Update(leaveType);
                await _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong.");
                return View(model);
            }
        }

        // GET: LeaveTypesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            //var leaveType = await _repo.FindById(id);
            var leaveType = await _unitOfWork.LeaveTypes.Find(_ => _.Id == id);

            if (leaveType == null)
            {
                return NotFound();
            }

            //var isSuccess = await _repo.Delete(leaveType);
            _unitOfWork.LeaveTypes.Delete(leaveType);
            await _unitOfWork.Save();           

            //if (!isSuccess)
            //{
            //    return BadRequest();
            //}

            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, LeaveTypeVM model)
        {
            try
            {
                //var leaveType = await _repo.FindById(id);
                var leaveType = await _unitOfWork.LeaveTypes.Find(_ => _.Id == id);

                if (leaveType == null)
                {
                    return NotFound();
                }

                //var isSuccess = await _repo.Delete(leaveType);
                _unitOfWork.LeaveTypes.Delete(leaveType);
                await _unitOfWork.Save();

                //if (!isSuccess)
                //{
                //    return View(model);
                //}

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}