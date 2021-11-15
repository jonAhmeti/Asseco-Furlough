using Furlough.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Furlough.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        { 
            return View(); 
        }

        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LoginPartial()
        {
            if (Request.Headers.ContainsKey("X-Requested-With")
                && Request.Headers["X-Requested-With"][0] == "XMLHttpRequest")
            {
                return PartialView("Partial/Login");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SignupPartial()
        {
            if (Request.Headers.ContainsKey("X-Requested-With")
                && Request.Headers["X-Requested-With"][0] == "XMLHttpRequest")
            {
                return PartialView("Partial/Signup");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}