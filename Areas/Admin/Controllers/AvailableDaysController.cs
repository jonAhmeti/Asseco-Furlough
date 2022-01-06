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

        // GET: AvailableDaysController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AvailableDaysController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AvailableDaysController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvailableDaysController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AvailableDaysController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvailableDaysController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AvailableDaysController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
