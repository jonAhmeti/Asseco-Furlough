using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {   
        public IActionResult Index()
        {
            var requests = new List<Furlough.DAL.Models.RequestByDepartment>{
                new DAL.Models.RequestByDepartment{
                    EmployeeName = "Chris",
                    RequestedOn = new DateTime(2021, 09, 05),
                    RequestStatusId = 2,
                },
                new DAL.Models.RequestByDepartment{
                    EmployeeName = "Chris",
                    RequestedOn = new DateTime(2021, 05, 24),
                    RequestStatusId = 1,
                },
                new DAL.Models.RequestByDepartment{
                    EmployeeName = "Chris",
                    RequestedOn = new DateTime(2021, 07, 15),
                    RequestStatusId = 3,
                },
                new DAL.Models.RequestByDepartment{
                    EmployeeName = "Chris",
                    RequestedOn = new DateTime(2021, 11, 13),
                    RequestStatusId = 2,
                },
                new DAL.Models.RequestByDepartment{
                    EmployeeName = "Chris",
                    RequestedOn = new DateTime(2021, 12, 23),
                    RequestStatusId = 3,
                }
            };

            ViewBag.requests = requests;

            return View();
        }
    }
}
