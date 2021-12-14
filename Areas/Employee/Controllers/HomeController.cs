using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRequest(IEnumerable<DateTime> dates)
        {
            
            return RedirectToAction("Index");
        }
    }
}
