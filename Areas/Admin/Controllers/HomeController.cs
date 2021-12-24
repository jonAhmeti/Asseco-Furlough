using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DAL.Request _contextRequest;
        private readonly DAL.Department _contextDepartment;

        public HomeController(DAL.Department contextDepartment, DAL.Request contextRequest)
        {
            _contextRequest = contextRequest;
            _contextDepartment = contextDepartment;
        }
        public IActionResult Index()
        {
            ViewBag.requests = _contextRequest.GetAllByRowCount(5);

             return View(_contextDepartment.GetAll());
        }
    }
}
