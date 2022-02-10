using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly DAL.Request _contextRequest;
        private readonly DAL.Department _contextDepartment;
        private readonly DAL.Employee _contextEmployee;

        public HomeController(DAL.Department contextDepartment, DAL.Request contextRequest, DAL.Employee contextEmployee)
        {
            _contextRequest = contextRequest;
            _contextDepartment = contextDepartment;
            _contextEmployee = contextEmployee;
        }
        public IActionResult Index()
        {
            ViewBag.requests = _contextRequest.GetAllByRowCount(5);
            ViewBag.departments = _contextDepartment.GetAll();
            ViewBag.employees = _contextEmployee.GetAll();
             return View();
        }
    }
}
