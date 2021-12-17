using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class RequestController : Controller
    {
        private DalMapper _dalMapper;
        private ViewModelMapper _vmMapper;
        private DAL.Request _contextRequest;
        private DAL.RequestType _contextRequestType;

        public RequestController(DalMapper dalMapper, ViewModelMapper vmMapper,
            DAL.Request contextRequest, DAL.RequestType contextRequestType )
        {
            _dalMapper = dalMapper;
            _vmMapper = vmMapper;

            _contextRequest = contextRequest;
            _contextRequestType = contextRequestType;
        }
        // GET: RequestController
        public ActionResult Index()
        {
            // requestStatusId: 1 - Pending, 2 - Approved, 3 - Rejected, 4 - Cancelled 
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value;
                var pendingRequests = new List<Models.Request>();
                foreach (var item in _contextRequest.GetByUser(int.Parse(userId), 1))
                {
                    pendingRequests.Add(_vmMapper.RequestMap(item));
                }

                ViewBag.RequestTypes = _contextRequestType.GetAll();
                return View(pendingRequests);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return RedirectToAction("Index", "Home", new { Area = "" });
            }
        }

        // GET: RequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestController/Create
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

        // GET: RequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RequestController/Edit/5
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

        // GET: RequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RequestController/Delete/5
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
