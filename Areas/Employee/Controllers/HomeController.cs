using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class HomeController : Controller
    {
        private DAL.Request _contextRequest;
        private DAL.RequestType _contextRequestType;
        private DalMapper _dalMapper;

        public HomeController(DAL.RequestType contextRequestType, DAL.Request contextRequest, DalMapper dalMapper)
        {
            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;

            _dalMapper = dalMapper;
        }

        public IActionResult Index()
        {
            ViewBag.requestTypes = _contextRequestType.GetAll();
            return View();
        }

        [HttpPost]
        public bool SubmitRequest(Models.Request request)
        {
            var result = false;
            try
            {
                request.RequestedByUserId = int.Parse(
                    HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);

                result = _contextRequest.Add(_dalMapper.DalRequestMap(request));
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return result;
            }
             return result;
        }
    }
}
