using Furlough.DAL;
using Furlough.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Furlough.SecurityHandlers;
using Microsoft.AspNetCore.Authentication;
using Furlough.Models.Mapper;

namespace Furlough.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DAL.User _contextUser;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Department _contextDepartment;
        private readonly DAL.Position _contextPosition;
        private readonly DAL.DepartmentPositions _contextDepartmentPositions;
        private readonly ViewModelMapper _vmMapper;

        public HomeController(ILogger<HomeController> logger, 
            DAL.User contextUser, DAL.Employee contextEmployee, 
            DAL.Department contextDepartment, DAL.DepartmentPositions contextDepartmentPositions, DAL.Position contextPosition,
            ViewModelMapper vmMapper)
        {
            _logger = logger;

            _contextUser = contextUser;
            _contextEmployee = contextEmployee;
            _contextDepartment = contextDepartment;
            _contextPosition = contextPosition;
            _contextDepartmentPositions = contextDepartmentPositions;

            _vmMapper = vmMapper;
        }

        [AllowAnonymous]
        public IActionResult Index(string? message = null)
        { 
            return View();
        }

        #region Login/Signup
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Models.User user)
        {
            var dbUser = _contextUser.GetByUsername(user.Username);
            if (dbUser == null) return Error();

            return Ok();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Signup(Models.SignupViewModel signupModel)
        {
            //var isValidEmail = (signupModel.Email.Split('@')[1] == "asseco-see.com");

            var userId = _contextUser.Add(new DAL.Models.User
            {
                Username = signupModel.Username,
                Password = signupModel.Password
            });

            _contextEmployee.Add(new DAL.Models.Employee
            {
                Email = signupModel.Email,
                UserId = (int)userId,
                Name = signupModel.Name,
                DepartmentId = signupModel.DepartmentId,
                PositionId = signupModel.PositionId
            });

            //Likely username already exists
            if (userId == 0)
            {
                return RedirectToAction("Index", routeValues: "Username is taken, please try another.");
            }


            return RedirectToAction("Index");
        }

        #endregion
        #region Partial Views Login/Signup
        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignupPartial()
        {
            if (Request.Headers.ContainsKey("X-Requested-With")
                && Request.Headers["X-Requested-With"][0] == "XMLHttpRequest")
            {
                var departments = new List<Models.Department>();
                foreach (var item in _contextDepartment.GetAll())
                {
                    departments.Add(_vmMapper.DepartmentMap(item));
                }

                ViewBag.departments = departments;
                return PartialView("Partial/Signup");
            }

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Models.Position>? GetPositions(string departmentId)
        {
            if (Request.Headers.ContainsKey("X-Requested-With")
                && Request.Headers["X-Requested-With"][0] == "XMLHttpRequest")
            {
                var departmentPositions = _contextDepartmentPositions.GetPositionsByDepartmentId(int.Parse(departmentId));
                var positions = new List<Models.Position>();
                foreach (var item in departmentPositions)
                {
                    positions.Add(_vmMapper.PositionMap(_contextPosition.GetById(item.PositionId)));
                }

                return positions;
            }
            return null;
        }

        #endregion
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