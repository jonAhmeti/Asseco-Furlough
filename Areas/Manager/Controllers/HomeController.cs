using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly DAL.Request _contextRequest;
        private readonly DAL.Employee _contextEmployee;

        public HomeController(DAL.Request contextRequest, DAL.Employee contextEmployee)
        {
            _contextRequest = contextRequest;
            _contextEmployee = contextEmployee;
        }
        public IActionResult Index()
        {
            ViewBag.requests = _contextRequest.GetAllByRowCount(5);
            ViewBag.employees = _contextEmployee.GetAll();
            return View();
        }
    }
}
