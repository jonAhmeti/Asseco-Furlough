using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class HomeController : Controller
    {
        private DAL.RequestType _contextRequestType;

        public HomeController(DAL.RequestType contextRequestType)
        {
            _contextRequestType = contextRequestType;
        }

        public IActionResult Index()
        {
            ViewBag.requestTypes = _contextRequestType.GetAll();
            return View();
        }

        [HttpPost]
        public bool SubmitRequest(Models.Request request)
        {
            
            return true;
        }
    }
}
