using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly DAL.RequestHistory _contextRequestHistory;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.AvailableDays _contextAvailableDays;
        private readonly ViewModelMapper _vmMapper;
        private readonly DalMapper _dalMapper;

        public RequestController(DAL.Request contextRequest, DAL.RequestType contextRequestType, DAL.User contextUser,
            DAL.Employee contextEmployee, DAL.RequestStatus contextRequestStatus, DAL.RequestHistory contextRequestHistory,
            DAL.AvailableDays contextAvailableDays,
            ViewModelMapper vmMapper, DalMapper dalMapper)
        {
            _contextUser = contextUser;
            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
            _contextRequestStatus = contextRequestStatus;
            _contextRequestHistory = contextRequestHistory;
            _contextEmployee = contextEmployee;
            _contextAvailableDays = contextAvailableDays;

            _vmMapper = vmMapper;
            _dalMapper = dalMapper;
        }

        // GET: Admin/Request
        public async Task<IActionResult> Index()
        {
            try
            {
                var requests = _contextRequest.GetAll();
                return View(requests);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        public async Task<IActionResult> Approved()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected
            var requests = _contextRequest.GetByStatusId(2);
            return View(requests);
        }

        public async Task<IActionResult> Rejected()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected
            var requests = _contextRequest.GetByStatusId(3);
            return View(requests);
        }

        public async Task<IActionResult> Cancelled()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected, 4 - Cancelled
            var requests = _contextRequest.GetByStatusId(4);
            return View(requests);
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
            ViewData["RequestType"] = _contextRequestType.GetById(request.RequestTypeId);
            ViewData["Employee"] = employee;
            return View(_vmMapper.RequestMap(request));
        }

        // POST: Admin/Request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }
            var employee = _vmMapper.EmployeeMap(_contextEmployee.GetByUserId(request.RequestedByUserId));
            if (employee == null)
            {
                return NotFound($"Employee with user id {request.RequestedByUserId} not found.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);

                    //this might need to be put in BLL later on
                    //save history before editing
                    var prevRequest = _contextRequest.GetById(id);
                    _contextRequestHistory.Add(new DAL.Models.RequestHistory
                    {
                        AlteredByUserId = loggedinUser,
                        RequestId = request.Id,
                        PreviousDates = prevRequest.Dates,
                        PreviousRequestStatusId = prevRequest.RequestStatusId,
                        PreviousRequestTypeId = prevRequest.RequestTypeId,
                    });

                    request.LUBUserId = loggedinUser;
                    if (request.RequestTypeId != prevRequest.RequestTypeId)
                    {
                        request.RequestTypeId = prevRequest.RequestTypeId;
                    }
                    //make edit
                    var result = _contextRequest.Edit(_dalMapper.DalRequestMap(request), prevRequest.RequestStatusId);
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
            ViewData["RequestType"] = _contextRequestType.GetById(request.RequestTypeId);
            ViewData["Employee"] = employee;
            return View(request);
        }

        // GET: Admin/Request/Delete/5
        public async Task<IActionResult> Delete(int id, bool fkError = false)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            ViewBag.FkError = fkError;
            return View(request);
        }

        // POST: Admin/Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var request = _contextRequest.DeleteById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                //most likely this request already has history of being changed/edited
                return RedirectToAction(nameof(Delete), new { fkError = true });
            }
        }
    }
}
