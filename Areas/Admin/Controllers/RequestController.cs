using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.DAL;
using Furlough.DAL.Models;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestController : Controller
    {
        private readonly DAL.User _contextUser;
        private readonly DAL.Request _contextRequest;
        private readonly DAL.RequestType _contextRequestType;
        private readonly DAL.RequestStatus _contextRequestStatus;
        private readonly DAL.Employee _contextEmployee;
        private readonly ViewModelMapper _vmMapper;
        private readonly DalMapper _dalMapper;

        public RequestController(DAL.Request contextRequest, DAL.RequestType contextRequestType, DAL.User contextUser,
            DAL.Employee contextEmployee, DAL.RequestStatus contextRequestStatus,
            ViewModelMapper vmMapper, DalMapper dalMapper)
        {
            _contextUser = contextUser;
            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
            _contextRequestStatus = contextRequestStatus;
            _contextEmployee = contextEmployee;

            _vmMapper = vmMapper;
            _dalMapper = dalMapper;
        }

        // GET: Admin/Request
        public async Task<IActionResult> Index()
        {
            var userDepartment = "";
            try
            {
                userDepartment = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value;
                var departmentRequests = _contextRequest.GetByDepartment(int.Parse(userDepartment));
                return View(departmentRequests);
            }
            catch (Exception e)
            {
                if (userDepartment == "")
                    return RedirectToAction("Index", "Home", new { Area = "" });
                  //return BadRequest("Not logged in, or this user doesn't belong to a department.");

                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        // GET: Admin/Request/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Admin/Request/Create
        public IActionResult Create()
        {
            ViewData["RequestStatusId"] = new SelectList(_contextRequestStatus.GetAll(), "Id", "Type");
            ViewData["RequestTypeId"] = new SelectList(_contextRequestType.GetAll(), "Id", "Type");
            ViewData["RequestedByUserId"] = new SelectList(_contextUser.GetAll(), "Id", "Username");
            return View();
        }

        // POST: Admin/Request/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateFrom,DateUntil,RequestedByUserId,RequestedOn,RequestStatusId,PaidDays,RequestTypeId")] Models.Request request)
        {
            if (ModelState.IsValid)
            {
                var result = _contextRequest.Add(_dalMapper.DalRequestMap(request));
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestStatusId"] = new SelectList(_contextRequestStatus.GetAll(), "Id", "Type", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_contextRequestType.GetAll(), "Id", "Type", request.RequestTypeId);
            ViewData["RequestedByUserId"] = new SelectList(_contextUser.GetAll(), "Id", "Username", request.RequestedByUserId);
            return View(request);
        }

        // GET: Admin/Request/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }
            var employee = _vmMapper.EmployeeMap(_contextEmployee.GetByUserId(request.RequestedByUserId));
            if (employee == null)
            {
                return NotFound($"Employee with user id {request.RequestedByUserId} not found.");
            }

            ViewData["RequestStatusId"] = new SelectList(_contextRequestStatus.GetAll(), "Id", "Type", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_contextRequestType.GetAll(), "Id", "Type", request.RequestTypeId);
            ViewData["Employee"] = employee;
            return View(request);
        }

        // POST: Admin/Request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateFrom,DateUntil,RequestedByUserId,RequestedOn,RequestStatusId,PaidDays,RequestTypeId")] Models.Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = _contextRequest.Edit(_dalMapper.DalRequestMap(request));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_contextRequest.GetById(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestStatusId"] = new SelectList(_contextRequestStatus.GetAll(), "Id", "Type", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_contextRequestType.GetAll(), "Id", "Type", request.RequestTypeId);
            ViewData["RequestedByUserId"] = new SelectList(_contextUser.GetAll(), "Id", "Username", request.RequestedByUserId);
            return View(request);
        }

        // GET: Admin/Request/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Admin/Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = _contextRequest.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
