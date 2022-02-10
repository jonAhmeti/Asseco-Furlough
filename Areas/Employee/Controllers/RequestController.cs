using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Admin")]
    public class RequestController : Controller
    {
        private readonly IStringLocalizer _localizer;
        private DalMapper _dalMapper;
        private ViewModelMapper _vmMapper;
        private readonly DAL.Request _contextRequest;
        private readonly DAL.RequestType _contextRequestType;
        private readonly DAL.AvailableDays _contextAvailableDay;
        private readonly DAL.RequestHistory _contextRequestHistory;

        public RequestController(DalMapper dalMapper, ViewModelMapper vmMapper, IStringLocalizerFactory localizer,
            DAL.Request contextRequest, DAL.RequestType contextRequestType, DAL.AvailableDays contextAvailableDay,
            DAL.RequestHistory contextRequestHistory)
        {
            //Check this again
            _localizer = localizer.Create(typeof(Resources.Areas.Employee.Views.Request.Index));

            _dalMapper = dalMapper;
            _vmMapper = vmMapper;

            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
            _contextAvailableDay = contextAvailableDay;
            _contextRequestHistory = contextRequestHistory;
        }
        // GET: RequestController
        public ActionResult Index()
        {
            // requestStatusId: 1 - Pending, 2 - Approved, 3 - Rejected, 4 - Cancelled 
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value;

                ViewBag.RequestTypes = _contextRequestType.GetAll();
                var requests = new List<Models.Request>();
                foreach (var item in _contextRequest.GetByUser(int.Parse(userId), 1))
                {
                    requests.Add(_vmMapper.RequestMap(item));
                }
                return View(requests);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return RedirectToAction("Index", "Home", new { Area = "" });
            }
        }

        public IActionResult Approved()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected
            var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
            var requests = new List<Models.Request>();
            foreach (var item in _contextRequest.GetOfEmployee(2, loggedinUser))
            {
                requests.Add(_vmMapper.RequestMap(item));
            }
            ViewBag.RequestTypes = _contextRequestType.GetAll();
            return View(requests);
        }

        public IActionResult Rejected()
        {
            // 1 - Pending, 2 - Approved, 3 - Rejected
            var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
            var requests = new List<Models.Request>();
            foreach (var item in _contextRequest.GetOfEmployee(3, loggedinUser))
            {
                requests.Add(_vmMapper.RequestMap(item));
            }
            ViewBag.RequestTypes = _contextRequestType.GetAll();
            return View(requests);
        }

        //PUT: RequestController/Edit/5
        [HttpPut]
        public IActionResult Edit(int id, Models.Request obj)
        {
            try
            {
                var prevRequest = _contextRequest.GetById(id);
                if (prevRequest == null)
                    return NotFound("Request wasn't found");

                if (prevRequest.Dates == obj.Dates)
                    return Ok("Nothing changed successfully");

                if (string.IsNullOrWhiteSpace(obj.Dates))
                    return BadRequest("Request dates can't be empty");

                obj.DaysAmount = obj.Dates.Split(",").Length;

                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                var loggedinEmployee = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);

                //this should be moved to BLL
                //check if user has available days
                var availableDays = _contextAvailableDay.GetByEmployeeId(loggedinEmployee);
                var leaveType = Enum.GetName(typeof(Models.Enums.RequestType), prevRequest.RequestTypeId);
                var daysOfType = (int?)typeof(DAL.Models.AvailableDay).GetProperty(leaveType).GetValue(availableDays);

                if (daysOfType < obj.DaysAmount)
                    return BadRequest("Not enough days left in this category");

                var daysDifference = prevRequest.DaysAmount - obj.DaysAmount;

                var result = _contextRequest.EditDates(id, obj.Dates, loggedinUser, daysDifference, leaveType);
                if (result) //add to RequestHistory
                {
                    var historyResult = _contextRequestHistory.Add(new DAL.Models.RequestHistory
                    {
                        AlteredByUserId = loggedinUser,
                        RequestId = id,
                        PreviousDates = prevRequest.Dates,
                        PreviousRequestStatusId = prevRequest.RequestStatusId,
                        PreviousRequestTypeId = prevRequest.RequestTypeId,
                    });
                }
                return result ? Ok("Request changed successfully") : StatusCode(500, "Something went wrong editing your request");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Something went wrong editting your request.");
            }
        }

        // POST: RequestController/Cancel/5
        [HttpDelete, ActionName("Cancel")]
        public ActionResult Delete(int id)
        {
            try
            {
                var request = _contextRequest.GetById(id);
                var loggedinUserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                var loggedinEmployee = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);

                if (request == null)
                    return NotFound("Request doesn't exist.");

                //if not the logged in user's request or the request isn't pending
                if (request.RequestedByUserId != loggedinUserId  || request.RequestStatusId != 1 )
                    return BadRequest("Something went wrong cancelling your request.");

                var result = _contextRequest.DeleteById(request.Id);

                //set back employeeDays based on the request daysAmount
                if (result)
                {
                    var availableDays = _contextAvailableDay.GetByEmployeeId(loggedinEmployee);
                    var requestType = _contextRequestType.GetById(request.RequestTypeId);
                    var availableRequestDays = (int)availableDays.GetType().GetProperty(requestType.Type).GetValue(availableDays);
                    var res = _contextAvailableDay.SetDays(loggedinEmployee, requestType.Type, availableRequestDays + request.DaysAmount);
                }

                var x = _localizer.GetAllStrings();
                return result ? Ok() : StatusCode(500, "Something went wrong cancelling your request.");
                //will return Error 500 most likely if Request status was previously changed by Admin and back to Pending
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return StatusCode(500);
            }
            
        }
    }
}
