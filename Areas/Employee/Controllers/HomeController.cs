using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
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
            ViewBag.availableDays = _contextAvailableDays.GetByEmployeeId(employeeId).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRequest(Models.Request request)
        {
            IActionResult result;

            try
            {
                var employeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);
                var availableDays = _contextAvailableDays.GetByEmployeeId(employeeId).FirstOrDefault();

                //This gets the type's property dynamically using the type of the request,
                //after which we get that property from availableDays and get it's value
                var daysLeft = availableDays.GetType().GetProperty(_contextRequestType.GetById(request.RequestTypeId).Type).GetValue(availableDays);
                if (daysLeft == null) return BadRequest();
                if (request.PaidDays > (int)daysLeft)
                {
                    result = BadRequest("Not enough days left.");
                    return result;
                }

                request.RequestedByUserId = int.Parse(
                    HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                
                result = _contextRequest.Add(_dalMapper.DalRequestMap(request)) ? Ok() : StatusCode(500, "Something went wrong while adding your request");
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
