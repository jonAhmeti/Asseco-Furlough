using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class RequestController : Controller
    {
        private DalMapper _dalMapper;
        private ViewModelMapper _vmMapper;
        private readonly DAL.Request _contextRequest;
        private readonly DAL.RequestType _contextRequestType;
        private readonly DAL.AvailableDays _contextAvailableDay;

        public RequestController(DalMapper dalMapper, ViewModelMapper vmMapper,
            DAL.Request contextRequest, DAL.RequestType contextRequestType, DAL.AvailableDays contextAvailableDay )
        {
            _dalMapper = dalMapper;
            _vmMapper = vmMapper;

            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
            _contextAvailableDay = contextAvailableDay;
        }
        // GET: RequestController
        public ActionResult Index()
        {
            // requestStatusId: 1 - Pending, 2 - Approved, 3 - Rejected, 4 - Cancelled 
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value;

                ViewBag.RequestTypes = _contextRequestType.GetAll();
                return View(_contextRequest.GetByUser(int.Parse(userId), 1));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return RedirectToAction("Index", "Home", new { Area = "" });
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

                return result ? Ok() : StatusCode(500, "Something went wrong cancelling your request.");
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
