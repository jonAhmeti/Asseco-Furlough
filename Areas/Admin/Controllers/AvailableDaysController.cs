using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Admin
{
    [Area("Admin")]
    public class AvailableDaysController : Controller
    {
        private readonly DAL.AvailableDays _contextAvailableDays;
        private readonly DAL.Employee _contextEmployee;

        public AvailableDaysController(DAL.AvailableDays contextAvailableDays, DAL.Employee contextEmployee)
        {
            _contextAvailableDays = contextAvailableDays;
            _contextEmployee = contextEmployee;
        }
        // GET: AvailableDaysController
        public ActionResult Index()
        {
            return View(_contextAvailableDays.GetAll());
        }
    }
}
