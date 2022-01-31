using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        private readonly DAL.Request _contextRequest;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Department _contextDepartment;

        public HomeController(DAL.Request contextRequest, DAL.Employee contextEmployee, DAL.Department contextDepartment)
        {
            _contextRequest = contextRequest;
            _contextEmployee = contextEmployee;
            _contextDepartment = contextDepartment;
        }
        public IActionResult Index()
        {
            var managerDepartmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            
            ViewBag.requests = _contextRequest.GetAllByRowCount(5, managerDepartmentId);
            ViewBag.employees = _contextEmployee.GetByDepartmentId(managerDepartmentId);
            ViewBag.Department = _contextDepartment.GetById(managerDepartmentId);
            return View();
        }
    }
}
