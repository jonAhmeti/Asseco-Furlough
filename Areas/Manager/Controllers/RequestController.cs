using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Furlough.Models.Mapper;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class RequestController : Controller
    {
        private readonly DAL.User _contextUser;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Request _contextRequest;
        private readonly DAL.RequestType _contextRequestType;
        private readonly DAL.RequestStatus _contextRequestStatus;
        private readonly DAL.RequestHistory _contextRequestHistory;
        private readonly DalMapper _dalMapper;
        private readonly ViewModelMapper _vmMapper;

        public RequestController(DAL.Request contextRequest, DAL.RequestType contextRequestType,
            DAL.RequestStatus contextRequestStatus, DAL.RequestHistory contextRequestHistory, DAL.User contextUser,
            DAL.Employee contextEmployee,
            DalMapper dalMapper, ViewModelMapper vmMapper)
        {
            _contextUser = contextUser;
            _contextEmployee = contextEmployee;
            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
            _contextRequestStatus = contextRequestStatus;
            _contextRequestHistory = contextRequestHistory;

            _dalMapper = dalMapper;
            _vmMapper = vmMapper;
        }

        // GET: Manager/Request
        public async Task<IActionResult> Index()
        {
            //Get DepartmentId by HttpContext
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value); 

            return View(_contextRequest.GetByDepartment(departmentId, 1));
        }

        public async Task<IActionResult> Approved()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            return View(_contextRequest.GetByDepartment(departmentId, 2));
        }
        public async Task<IActionResult> Rejected()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected
            var departmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            return View(_contextRequest.GetByDepartment(departmentId, 3));
        }

        // GET: Manager/Request/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var request = _contextRequest.GetById(id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Manager/Request/Create
        public IActionResult Create()
        {
            var loggedinDepartment = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);

            ViewData["RequestStatusId"] = new SelectList(_contextRequestStatus.GetAll(), "Id", "Type");
            ViewData["RequestTypeId"] = new SelectList(_contextRequestType.GetAll(), "Id", "Type");
            ViewData["RequestedByUserId"] = new SelectList(_contextUser.GetByDepartmentId(loggedinDepartment), "Id", "Username");
            return View();
        }

        // POST: Manager/Request/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Request request)
        {
            if (ModelState.IsValid)
            {
                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                request.LUBUserId = loggedinUser;

                var result =  _contextRequest.Add(_dalMapper.DalRequestMap(request));
                return RedirectToAction(nameof(Index));
            }
            var loggedinDepartment = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);

            ViewData["RequestStatusId"] = new SelectList(_contextRequestStatus.GetAll(), "Id", "Type", request.RequestStatusId);
            ViewData["RequestTypeId"] = new SelectList(_contextRequestType.GetAll(), "Id", "Type", request.RequestTypeId);
            ViewData["RequestedByUserId"] = new SelectList(_contextUser.GetByDepartmentId(loggedinDepartment), "Id", "Username", request.RequestedByUserId);
            return View(request);
        }

        // GET: Manager/Request/Edit/5
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

        // POST: Manager/Request/Edit/5
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

        // GET: Manager/Request/Delete/5
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
