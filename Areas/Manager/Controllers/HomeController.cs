using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Manager")]
    public class HomeController : Controller
    {
        private readonly DAL.Request _contextRequest;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Department _contextDepartment;
        private readonly DAL.DepartmentPositions _contextDepartmentRoles;

        public HomeController(DAL.Request contextRequest, DAL.Employee contextEmployee, DAL.Department contextDepartment, DAL.DepartmentPositions contextDepartmentPositions)
        {
            _contextRequest = contextRequest;
            _contextEmployee = contextEmployee;
            _contextDepartment = contextDepartment;
            _contextDepartmentRoles = contextDepartmentPositions;
        }
        public IActionResult Index()
        {
            var managerDepartmentId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Department").Value);
            
            ViewBag.requests = _contextRequest.GetAllByRowCount(5, managerDepartmentId);
            ViewBag.employees = _contextEmployee.GetByDepartmentId(managerDepartmentId);
            ViewBag.Department = _contextDepartment.GetById(managerDepartmentId).Name;
            ViewBag.Position = _contextDepartmentRoles.GetPositionsByDepartmentId(managerDepartmentId);
            return View();
        }
    }
}
