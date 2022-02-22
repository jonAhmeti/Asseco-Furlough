using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private DAL.Request _contextRequest;
        private DAL.RequestType _contextRequestType;
        private DAL.AvailableDays _contextAvailableDays;
        private DalMapper _dalMapper;

        public HomeController(DalMapper dalMapper, 
            DAL.RequestType contextRequestType, DAL.Request contextRequest, DAL.AvailableDays contextAvailableDays )
        {
            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
            _contextAvailableDays = contextAvailableDays;

            _dalMapper = dalMapper;
        }

        public IActionResult Index()
        {
            var employeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);
            ViewBag.requestTypes = _contextRequestType.GetAll();
            ViewBag.availableDays = _contextAvailableDays.GetByEmployeeId(employeeId);
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRequest(Models.Request request)
        {
            IActionResult result;

            try
            {
                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                var employeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);
                var availableDays = _contextAvailableDays.GetByEmployeeId(employeeId);

                //This gets the type's property dynamically using the type of the request,
                //after which we get that property from availableDays and get it's value
                var daysLeft = availableDays.GetType().GetProperty(_contextRequestType.GetById(request.RequestTypeId).Type).GetValue(availableDays);
                if (daysLeft == null) return BadRequest();

                //RequestTypeId = 8 is Unpaid Type
                if (request.RequestTypeId != 8 && (request.DaysAmount > (decimal)daysLeft || request.DaysAmount < 0))
                {
                    result = BadRequest("Not enough days left.");
                    return result;
                }

                request.RequestedByUserId = loggedinUser;
                request.LUBUserId = loggedinUser;


                //new value of days, adds instead of deducting if request type is unpaid (meaning unpaid leave days only increase, others decrease)
                var newDaysValue = (request.RequestTypeId == 8) ? (decimal)daysLeft + request.DaysAmount : (decimal)daysLeft - request.DaysAmount;

                result = _contextRequest.Add(_dalMapper.DalRequestMap(request)) ? Ok() : StatusCode(500, "Something went wrong while adding your request");
                var dbResult = _contextAvailableDays    //deduct days left from requested paidDays
                    .SetDays(employeeId, _contextRequestType.GetById(request.RequestTypeId).Type, newDaysValue);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                result = StatusCode(500);
                return result;
            }
             return result;
        }
    }
}
