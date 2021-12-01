using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Manager.Controllers
{
    public class DepartmentRolesController : Controller
    {
        private readonly DAL.DepartmentRoles _contextDepartmentRoles;

        public DepartmentRolesController(DAL.DepartmentRoles contextDepartmentRoles)
        {
            _contextDepartmentRoles = contextDepartmentRoles;
        }

        // GET: DepartmentRolesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DepartmentRolesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentRolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentRolesController/Create
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

        // GET: DepartmentRolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DepartmentRolesController/Edit/5
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

        // GET: DepartmentRolesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentRolesController/Delete/5
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
