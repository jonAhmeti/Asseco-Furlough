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
            var managerDepartmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            
            ViewBag.requests = _contextRequest.GetAllByRowCount(5, managerDepartmentId);
            ViewBag.employees = _contextEmployee.GetByDepartmentId(managerDepartmentId);
            return View();
        }
    }
}
