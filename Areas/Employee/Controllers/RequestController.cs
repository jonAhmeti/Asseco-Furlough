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
        private DAL.Request _contextRequest;
        private DAL.RequestType _contextRequestType;

        public RequestController(DalMapper dalMapper, ViewModelMapper vmMapper,
            DAL.Request contextRequest, DAL.RequestType contextRequestType )
        {
            _dalMapper = dalMapper;
            _vmMapper = vmMapper;

            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
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
        [HttpPost, ActionName("Cancel")]
        public ActionResult Delete(int id)
        {
            try
            {
                var request = _contextRequest.GetById(id);
                var loggedinUserId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);

                if (request == null)
                    return NotFound("Request doesn't exist.");

                //if not the logged in user's request
                if (request.RequestedByUserId != loggedinUserId)
                    return BadRequest("Something went wrong cancelling your request.");

                var result = _contextRequest.DeleteById(request.Id);
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
